## *참고
<a href='https://www.youtube.com/watch?v=ETYzjbnLixY&list=PLO-mt5Iu5TeYtWvM9eN-xnwRbyUAMWd3b&index=1'>2D 종스크롤 슈팅 - 플레이어 이동 구현하기 [유니티 기초 강좌 B27 + 에셋 다운로드]</a>  
<a href='https://github.com/lubiksss/ShootingGame'>블로그</a>

Unity로 Shooting Game을 만들었습니다. 튜토리얼을 거쳐서 이번엔 정말 게임다운 게임을 만든 것 같습니다. 고민도 정말 많이 했고 게임을 만드는 기술도 많이 배웠습니다. 이번 프로젝트가 지금까지 배운 모든 것들의 집약체이기 때문에 이번 프로젝트는 작성한 스크립트나 배운 기술들을 정리해보도록 하겠습니다.

## Preview
<div>
    <img src="https://user-images.githubusercontent.com/67966414/134112841-efaa7add-e09b-48a3-94ee-62b45b7035f9.JPG" alt="1" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/134112859-71a74288-854f-4661-bc1e-f28719c68b27.JPG" alt="2" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/134112866-d5385e1b-ae90-4121-8888-b6660e6b3b4b.JPG" alt="3" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/134112867-86ce904e-c941-420b-93fc-f4d633e13cc3.JPG" alt="4" style="width:24%;"/>
</div>
<div>
    <img src="https://user-images.githubusercontent.com/67966414/134112869-24a13762-3d31-486c-8f0e-9e11587e0ea4.JPG" alt="Boss1" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/134112870-e127eb78-8cec-4110-8497-2782c5f4c264.JPG" alt="Boss2" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/134112872-6b95be35-ff90-43ec-a73a-80d064939661.JPG" alt="Boss3" style="width:24%;"/>
    <img src="https://user-images.githubusercontent.com/67966414/134112874-f6afc46a-6673-4185-8f3d-c1a75a0fcb0e.JPG" alt="Boss4" style="width:24%;"/>
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
1. Move(없음)  
2. Fire  
3. OnTriggerEnter2D  

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