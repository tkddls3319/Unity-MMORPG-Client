# My-MMORPG-Client ( Unity MMO Client 프로젝트 )
해당 스토리지는 저의 GitHub의 Unity-MMO-RPG-Total 스토리지에서 Client 부분만 분리해 놓은 저장소입니다. Server와 Client, 패킷 자동화 프로그램은 Unity-MMO-RPG-Total저장소에 있습니다.
프로젝트를 진행하며 Unity의 전반적인 설계와 유지보수성, 다양한 디자인 패턴을 효과적으로 사용하는 방법에 대해 고민했습니다.
IOCP 서버와 Unity 클라이언트 간의 통신을 구현하여, 패킷 송수신과 관련된 기술을 포함합니다.
주요 기능

MMO 클라이언트 기능:
실시간 통신: 각 클라이언트는 서버를 통해 실시간으로 데이터를 주고받습니다. 일반 삼성 노트북 기준 1000명 이상 클라이언트를 서버와 연결 가능
게임방 관리, 새로운 게임방 생성, 기존 게임방에 참여 가능, 캐릭터 이동 및 공격, A-W-S-D 키를 사용하여 캐릭터 이동, SPACE 키로 공격 모션 실행.
실시간 채팅: 게임방 내의 유저들 간 대화 기능 제공.

통신 설계:
IOCP 서버와 Unity 클라이언트 간의 안정적이고 효율적인 패킷 송수신 통신 구현.
Unity의 단일 스레드 구조에서 패킷 처리와 이벤트 동기화 기술 포함.

프로젝트 목표
Unity 설계와 유지보수성 향상:
유지보수가 쉬운 프로젝트 구조 설계.
다양한 디자인 패턴 적용을 통한 확장성과 효율성 확보.

게임 개발 경험 축적:
MMO 클라이언트 개발 과정을 통해 Unity와 네트워크 프로그래밍에 대한 경험 확대.

사용 기술
Unity: 클라이언트 개발 및 게임 플레이 로직 구현.
IOCP 서버: 고성능 비동기 통신을 위한 서버 기술.
디자인 패턴:
Observer 패턴, Command 패턴, Status 패턴, Singleton 패턴: 전역 객체 관리.

멀티플레이 경험: 여러 사용자가 같은 게임방에서 실시간으로 상호작용.
채팅 시스템: 게임방 내에서 유저 간 실시간 채팅 가능.
유연한 설계: 유지보수와 확장을 고려한 설계 및 다양한 디자인 패턴 적용.

mmoRPG 클라이언트 최종본
![Lobby](https://github.com/tkddls3319/My-MMORPG-Client/assets/54829486/40da8c9a-3729-4947-807f-6784cbf4b4f4)
![Game](https://github.com/tkddls3319/My-MMORPG-Client/assets/54829486/8a31a323-f72b-4046-978c-01ee3966b24a)

https://github.com/tkddls3319/My-MMORPG-Client/assets/54829486/d5026087-c4fc-49fa-9784-d0226afd0a12

https://github.com/tkddls3319/My-MMORPG-Client/assets/54829486/ee0cc47b-7455-474d-8bc0-194cdb9ff70a
