# Udon-Sharp-Scripts
VRCSDK3(Udon) + UdonSharp���œ��삷��X�N���v�g����u���Ă��܂�

## Requirement
�E VRCSDK3(https://vrchat.com/home/download)
�E UdonSharp(https://github.com/Merlin-san/UdonSharp)
�E Unity2018.4.20f1

## FromCornerToCorner
Udon�ŋ����w�K������T���v���ł��B��������E��Ƀ}�����ړ�����悤�Ɋw�K���Ă����܂��B

### Deployment
1. Project��VRCSDK3��UdonSharp�𓱓�
2. FromCornerToCorner.prefab��Scene�ɔz�u
3. FromCornerToCorner(GameObject)��FromCornerToCorner.cs(Udon C# script)��ݒ�
4. Force Compile Script�����s

### Parameters
�E Alpha�F�w�K��(Learning rate)
�E Gamma�F������(Discount rate)
�E Epsilon : ��-greedy�@�ɗp����
�E DecayTime : Epsilon�̌�������(Epsilon = Epsilon*(1-step/DecayTime))