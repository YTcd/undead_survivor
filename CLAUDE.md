# Undead Survivor — CLAUDE.md

## 프로젝트 개요

뱀파이어 서바이버 라이크 2D 액션 게임. Unity 6으로 개발 중인 초기 프로토타입.

- **엔진:** Unity 6000.2.12f1
- **렌더링:** Universal Render Pipeline (URP) 2D
- **입력:** Unity New Input System
- **카메라:** Cinemachine 3.x

## 폴더 구조

```
Assets/Undead Survivor/
├── Script/        # C# 게임 로직
├── Animations/    # 애니메이터 컨트롤러 및 클립
├── Sprites/       # 캐릭터·타일 스프라이트
├── Tiles/         # 타일맵용 타일 에셋
├── Audio/         # BGM 및 SFX
├── Fonts/         # neodgm.ttf (한글 픽셀 폰트)
└── Demo/          # 메인 플레이어블 씬
```

## 스크립트 구조

| 파일 | 역할 |
|------|------|
| `GameManager.cs` | Singleton. `GameManager.instance`로 전역 접근. Player 레퍼런스 보유 |
| `Player.cs` | 플레이어 이동(Rigidbody2D.MovePosition), 입력 처리, 애니메이션 |
| `Reposition.cs` | 플레이어가 Area 트리거를 벗어나면 Ground/Enemy 오브젝트를 재배치해 무한 맵 구현 |

## 핵심 패턴 및 컨벤션

- **싱글톤:** `GameManager.instance`로 글로벌 상태 접근
- **무한 맵:** `Reposition.cs`가 `OnTriggerExit2D`를 이용해 40유닛 단위로 타일 재배치
- **태그:** `"Ground"` (타일), `"Area"` (카메라 트리거 존), `"Enemy"` (적)
- **애니메이터 파라미터:** `"Speed"` float — 0이면 대기, 0 초과면 이동 애니메이션
- **이동 구현:** `rigid.MovePosition` 사용 (force/velocity 방식은 주석으로 남겨둠)
- **멤버 변수 접두사:** `m_` (예: `m_player`, `m_speed`)

## 구현된 기능

- [x] 플레이어 4방향 이동 + 스프라이트 좌우 반전
- [x] 이동/대기 애니메이션 전환
- [x] Cinemachine 카메라 추적
- [x] 타일맵 기반 무한 스크롤 맵
- [x] 5종 적 스프라이트·애니메이션 임포트
- [x] BGM + SFX 에셋 임포트 (LevelUp, Hit, Dead, Win, Lose 등)

## 미구현 / 다음 작업

- 적 스폰 및 AI (이동, 플레이어 추적)
- `Reposition.cs`의 Enemy 케이스 구현
- 무기/공격 시스템 (Melee, Range)
- 레벨업 및 능력치 강화 UI
- HP, 점수, 게임오버 로직
- 플레이어 캐릭터 선택 (Farmer 1~3 변형)

## 유의사항

- **씬 편집은 Claude가 직접 불가** — `.unity` 씬 파일은 직접 수정하지 말 것. 스크립트(.cs) 작성 후 Unity 에디터에서 씬에 붙이는 방식으로 진행
- **컴파일 오류 확인** — 스크립트 수정 후 Unity 에디터 Console에서 직접 확인 필요
- **프리팹/에셋 수정** — `.prefab`, `.asset` 파일은 YAML 포맷이므로 수동 편집 위험. 코드로만 처리할 것
- **태그/레이어 추가** — `ProjectSettings/TagManager.asset`에서 확인 가능하나, 실제 추가는 Unity 에디터에서 할 것
