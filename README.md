# Udon-Sharp-Scripts
VRCSDK3(Udon) + UdonSharp環境で動作するスクリプト等を置いています

## Requirement
* VRCSDK3(https://vrchat.com/home/download)
* UdonSharp(https://github.com/Merlin-san/UdonSharp)
* Unity2018.4.20f1

## FromCornerToCorner
Udonで強化学習をするサンプルです。左下から右上にマルが移動するように学習していきます。

### Deployment
1. ProjectにVRCSDK3とUdonSharpを導入
2. FromCornerToCorner.prefabをSceneに配置
3. FromCornerToCorner(GameObject)のFromCornerToCorner.cs(Udon C# script)を設定
4. Force Compile Scriptを実行

### Parameters
* Alpha：学習率(Learning rate)
* Gamma：割引率(Discount rate)
* Epsilon : ε-greedy法に用いる
* DecayTime : Epsilonの減衰時間(Epsilon = Epsilon*(1-step/DecayTime))