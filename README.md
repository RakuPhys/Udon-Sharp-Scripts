# Udon-Sharp-Scripts
VRCSDK3(Udon) + UdonSharp���œ��삷��X�N���v�g����u���Ă��܂�
##Requirement
�EVRCSDK3(https://vrchat.com/home/download)
�EUdonSharp(https://github.com/Merlin-san/UdonSharp)
�EUnity2018.4.20f1
##FromCornerToCorner
Udon�ŋ����w�K������T���v���ł��B��������E��Ƀ}�����ړ�����悤�Ɋw�K���Ă����܂��B
### Deployment
1. Project��VRCSDK3��UdonSharp�𓱓�
2. FromCornerToCorner.prefab��Scene�ɔz�u
3. FromCornerToCorner(GameObject)��FromCornerToCorner.cs(Udon C# script)��ݒ�
4. Force Compile Script�����s
### Parameters
�EAlpha�F�w�K��(Learning rate)
�EGamma�F������(Discount rate)
�EEpsilon : ��-greedy�@�ɗp����
�EDecayTime : Epsilon�̌�������(Epsilon = Epsilon*(1-step/DecayTime))