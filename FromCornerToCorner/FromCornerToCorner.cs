
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class FromCornerToCorner : UdonSharpBehaviour
{
    [SerializeField] private GameObject boardObj;
    [SerializeField] private float alpha = 0.2f;//Learning rate
    [SerializeField] private float gamma = 0.95f;//Discount rate
    [SerializeField] private float epsilon = 1.0f;//epsilon-greedy
    [SerializeField] private float decayTime = 500.0f;//epsilon decay factor

    private Material boardMat;
    private float[] qTable = new float[64];
    private float waitTime1 = 0.0f;//
    private float waitTime2 = 0.0f;//for game end
    private int state = 0;//0-15 or <0(overrange)
    private int lastState = 0;//0-15 or <0(overrange),buffer
    private int act = 0;//0-3(0:down,1:right,2:up,3:left)
    private int lastAct = 0;//0-3,buffer
    private bool goal = false;
    private bool fail = false;
    private bool start = true;
    private bool first = true;
    private int gameCount = 0;

    void Update()
    {
        if (first)
        {
            boardMat = boardObj.GetComponent<MeshRenderer>().material;
            InitQTable();
            first = false;
        }
        waitTime1 += Time.deltaTime;
        if (waitTime1 >0.05f&&start)
        {
            Act();
            Judge();
            Train();
            Render();//set state and result to shader
            if (goal | fail)
            {
                state = 0;
                goal = false;
                fail = false;
                start = false;
                gameCount += 1;
                epsilon = epsilon - epsilon/decayTime;
                //Debug.Log(epsilon);
            }
            waitTime1 = 0.0f;
        }
        if (!start)
        {
            waitTime2 += Time.deltaTime;
            if(waitTime2 > 0.1f)
            {
                start = true;
                waitTime1 = 0.0f;
                waitTime2 = 0.0f;
                Render();//set state and result to shader
            }
        }
    }
    private void Act()
    {
        lastState = state;
        lastAct = act;
        act = GetAct();
        state = NextState(state, act);
    }
    private void Judge()
    {
        if (state < 0)
        {
            fail = true;
        }
        if (state == 15)
        {
            goal = true;
        }
    }
    private void Train()
    {
        float reward = GetReward();
        int Qid = lastState * 4 + act;//Q(st,at)=>Q(st*4+at)
        
        if (goal | fail)
        {
            qTable[Qid] = qTable[Qid] + alpha * (reward - qTable[Qid]);
        }
        else
        {
            int nextQid = state * 4 + QTableSelect();//max(Q(st+1,at+1))
            qTable[Qid] = qTable[Qid] + alpha * (reward + gamma * qTable[nextQid] - qTable[Qid]);
        }
    }
    private void Render()
    {
        boardMat.SetFloat("_posx", state % 4);
        boardMat.SetFloat("_posy", (int)(state / 4));
        if (fail)
        {
            boardMat.SetFloat("_result", -1.0f);
        }
        else if (goal)
        {
            boardMat.SetFloat("_result", 1.0f);
        }
        else
        {
            boardMat.SetFloat("_result", 0.0f);
        }
    }
    private void InitQTable()
    {
        for (int i = 0; i < 64; i++)
        {
            qTable[i] = Random.Range(-0.1f, 0.1f);
        }
    }
    private int GetAct()
    {
        //epsilon-greedy
        int a = 0;
        if (Random.Range(0.0f, 1.0f) < epsilon)
        {
            a = Random.Range(0, 3);
        }
        else
        {
            a = QTableSelect();
        }
        return a;
    }
    private int QTableSelect()
    {
        int maxa = 0;
        float tempq = -1.0f;
        float q = 0.0f;
        Debug.Log("****");
        for (int a = 0; a < 4; a++)
        {
            q = qTable[state * 4 + a];
            Debug.Log(q);
            if (tempq < q)
            {
                tempq = q;
                maxa = a;
            }
        }
        return maxa;
    }
    private float GetReward()
    {
        float reward = 0.0f;
        if (goal)
        {
            reward = 1.0f;
        }
        else if (fail)
        {
            reward = -1.0f;
        }
        else
        {
            reward = -0.1f;
        }
        return reward;
    }
    private int NextX(int state,int act)
    {
        int x = state % 4;        
        if (act == 1)
        {
            x += 1;
        }   
        if (act == 3)
        {
            x -= 1;
        }        
        if (x > 3 || x < 0)
        {
            x -= 16;//fail
        }
        return x;
    }
    private int NextY(int state, int act)
    {
        int y = (int)(state/4);        
        if (act == 0)
        {
            y -= 1;
        }
        if (act == 2)
        {
            y += 1;
        }        
        if (y > 3 || y < 0)
        {
            y -= 16;//fail
        }
        return y;
    }
    private int NextState(int state, int act)
    {
        state = NextX(state, act) + 4 * NextY(state, act);
        return state;
    }
}
