## UIBuilder Tool

![image](https://user-images.githubusercontent.com/31693348/140470164-91070a95-9d09-418c-8356-5299247aab53.png)

1. ### USS 뷰

   - 현재 선택된 UXML에 사용될 USS들이 들어있고 그 USS에 스타일을 수정할 수 있음

   - #element

     - 으로 시작해서 생성하면, UXML 뷰에 있는 #으로 시작하는 element들과 이름을 맞출 수 있고 이름을 맞추게 되면 해당 이름을 쓰고있는 모든 element들이 일괄적으로 변경이 된다

   - .class

     -  으로 시작해서 생성하면, UI Toolkit Debugger의 PickElement를 이용해서 element를 이루고있는 클래스 이름을 알 수 있는데 이 클래스 이름으로 접근을 할 수 있다.
     -  AddToClassList("ABC");  이렇게 코드에서도 클래스를 넣어줄 수 있다.
     - ![image](https://user-images.githubusercontent.com/31693348/140470821-c10df939-24fd-499d-971c-60b89a5310ae.png)

   - PseudoStates

     - ![image](https://user-images.githubusercontent.com/31693348/140471371-e52f7f2b-ba4b-4f42-a8a1-4650429e7f3d.png)
     - 해당 클래스의 선택, 호버 등의 상태들을 나타내는데 이것도 상태에따라 스타일을 변경시켜 줄 수 있다
     - ![image](https://user-images.githubusercontent.com/31693348/140471493-70dc3108-923c-4278-9fb8-033ac385c2f8.png)
     - https://docs.unity3d.com/2020.1/Documentation/Manual/UIE-USS-Selectors-Pseudo-Classes.html 상태들이 있는데 사용되지 않는 상태들도있다. 최신화 되지않은듯

     

2. ### UXML 뷰

   - 전체적인 UXML 레이아웃을 변경할 수 있다
   - Library 뷰 로부터 레이아웃을 가져와 배치할 수 있다

   

3. ### Library 뷰

   - Standard

     - UXML뷰에서 현재 선택된 UXML을 클릭하면 인스펙터창에 Editor Extension Authoring 이 있는데
     - ![image-20211105160709851](C:\Users\basso\AppData\Roaming\Typora\typora-user-images\image-20211105160709851.png)
     - 이를 이용해서 더 많은 레이아웃을 사용할 수 있다

   - Project

     - 클래스 내부에

     - ```c#
       public new class UxmlFactory : UxmlFactory<이름, TwoPaneSplitView.UxmlTraits> { }
       ```

     - 을 사용하면 해당 프로젝트에 CustomControls가 생기게 된다.

