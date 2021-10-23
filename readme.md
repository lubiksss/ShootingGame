*****<a href='https://github.com/lubiksss/ShootingGame/tree/master/Assets/Script'>실제 작성한 C# Scripts</a>*****

## *참고
<!-- <a href='https://www.youtube.com/watch?v=ETYzjbnLixY&list=PLO-mt5Iu5TeYtWvM9eN-xnwRbyUAMWd3b&index=1'>2D 종스크롤 슈팅 - 플레이어 이동 구현하기 [유니티 기초 강좌 B27 + 에셋 다운로드]</a>   -->
<a href='https://lubiksss.github.io/unity/Unity_ShootingGame/'>블로그</a>

Unity로 Shooting Game을 만들었습니다. 튜토리얼을 거쳐서 이번엔 정말 게임다운 게임을 만든 것 같습니다. 고민도 정말 많이 했고 게임을 만드는 기술도 많이 배웠습니다. 이번 프로젝트가 지금까지 배운 모든 것들의 집약체이기 때문에 이번 프로젝트는 작성한 스크립트나 배운 기술들을 정리해보도록 하겠습니다.

## Preview
<div>
    <img src="https://user-images.githubusercontent.com/67966414/137348947-b342bf9c-f3a4-43b3-8a1a-f8c0c11788dc.jpg" alt="1" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/137348954-27063584-ce64-4c85-855b-4740622f4128.jpg" alt="2" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/137348955-6dbdc93b-a1ef-4cfa-a07b-e42304c37c73.jpg" alt="3" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/137348958-cc15c754-1df0-4f04-93e6-390c6ea2090a.jpg" alt="4" style="width:24%;"/>
</div>
<div>
    <img src="https://user-images.githubusercontent.com/67966414/137348963-348aaa7f-4be9-4b00-80e5-9c65433e748c.jpg" alt="Boss1" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/137348965-ea445288-df5d-4368-bc2a-657b6141f7b7.jpg" alt="Boss2" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/137348968-ebee1cad-ef5b-48c7-bb79-0baf33b3332f.jpg" alt="Boss3" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/137348970-0652d247-eef4-4a8e-a466-a1e75f360c4e.jpg" alt="Boss4" style="width:24%;"/>
</div>

## Unity 기본
### 객체의 컴포넌트 제어
<img src="https://user-images.githubusercontent.com/67966414/134113892-fafce52f-63b8-48f8-9d8b-2f86af1c5359.png" alt="Component" style="margin-left: auto; margin-right: auto; display: block">

### 객체에서 다른 객체를 제어
<img src="https://user-images.githubusercontent.com/67966414/134115486-10fd4a05-41ce-4066-b083-3bc676f7ca85.png" alt="GameObject" style="margin-left: auto; margin-right: auto; display: block">
<br>

## 객체별 분석
### Player
1. Move  
2. Fire  
3. Boom  
4. OnTriggerEnter2D  

### Enemy
1. Fire  
2. OnTriggerEnter2D  

### Boss(Enemy)
1. FireForward
2. FireShot
3. FireArc
4. FireAround

### GameManager
1. SpawnEnemy  
2. RespawnPlayer  
3. StageStart
4. StageEnd
5. GameOver
6. GameRetry
7. 각종 UI 업데이트

### ObjectManager
1. Generate  
2. MakeObj  
3. GetPool
