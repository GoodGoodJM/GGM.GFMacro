# GGM.GFMarco

GFMacro는 두개의 프로젝트로 나누어져 있습니다.

- GGM.GFMacro
- GGM.GFMacro.Core

## GGM.GFMacro

GGM.GFMacro는 소녀전선의 군수지원 매크로이며, GGM.GFMacro를 사용합니다.
현재 개발 진행중이며, 추후 보다 유연한 매크로 개발이 될 수 있도록 기능을 추가할 예정입니다.
동시에 GGM.GFMacro.Core의 예제 프로젝트이기도 합니다.

### 진행될 작업 목록

- [ ] 기본적인 군수지원 매크로 로직 구현.
- [ ] 스트립트 혹은 XML 을 이용하여 매크로 로직을 짤 수 있게끔 기능 추가.
- [ ] 플러그인 방식으로 새로운 기능을 추가할 수 있게끔 기능 추가.

## GGM.GFMacro.Core

GGM.GFMacro는 Nox, Momo등의 AppPlayer를 핸들링하기 위해 래핑된 라이브러리입니다.
기본적인 매크로를 구현하는데 필요한 ScreenCapture와 ClickEvent, 그리고 OpenCV를 이용한 이미지 유사도 측정 헬퍼 함수들이 포함되어 있습니다.
GGM.GFMacro.Core는 Windows 8.1 이상의 환경에서만 동작하며, 현재 Nox AppPlayer만 지원합니다.

### 진행될 작업 목록

- [ ] Momo AppPlayer 지원.
- [ ] 다양한 TouchEvent 추가.
  - [ ] DragEvent