# Udon-Sharp-Scripts
VRCSDK3(Udon) + UdonSharp���œ��삷��X�N���v�g����u���Ă��܂�

## Requirement
* VRCSDK3(https://vrchat.com/home/download)
* UdonSharp(https://github.com/Merlin-san/UdonSharp)
* Unity2018.4.20f1

## FromCornerToCorner
Udon�ŋ����w�K������T���v���ł��B��������E��Ƀ}�����ړ�����悤�Ɋw�K���Ă����܂��B

### Deployment
1. Project��VRCSDK3��UdonSharp�𓱓�
2. FromCornerToCorner.prefab��Scene�ɔz�u
3. FromCornerToCorner(GameObject)��FromCornerToCorner.cs(Udon C# script)��ݒ�
4. Force Compile Script�����s

### Parameters
* Alpha�F�w�K��(Learning rate)
* Gamma�F������(Discount rate)
* Epsilon : ��-greedy�@�ɗp����
* DecayTime : Epsilon�̌�������(Epsilon = Epsilon*(1-step/DecayTime))