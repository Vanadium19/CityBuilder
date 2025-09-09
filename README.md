# 📖 README.md

## 🏙 Mini City Builder (Test Day)

Учебный прототип по заданию **RC Test Day (2025)**.  
Цель — реализовать минимальный **city builder** с архитектурой Clean Architecture и разделением на слои.

---

## 🔧 Используемые библиотеки и инструменты
- **Unity 6** – движок  
- **UI Toolkit** – UI (панель ресурсов, выбор зданий, кнопки действий)  
- **VContainer** – Dependency Injection  
- **MessagePipe** – шина событий  
- **R3 (UniRx Next)** – реактивные стримы  
- **UniTask** – асинхронность (таймеры дохода, автосейвы)  
- **Input System** – управление камерой, хоткеи  
- **TriInspector** – удобная настройка инспектора  

---

## 🗂 Архитектура и структура проекта
Проект выполнен в стиле **Clean Architecture**, каждый слой вынесен в отдельную сборку (.asmdef).  

```
Assets/
 ├─ Domain/                // Чистые модели (Building, Economy, GridPosition)
 ├─ Application/           // Use Cases (Place, Move, Remove, Upgrade), сервисы экономики
 ├─ Presentation/          // UI Toolkit (HudView, BuildingView, Presenters)
 └─ Infrastructure/        // Save/Load, Input Adapter, фабрики, DI (VContainer)
```

- **Domain** – бизнес-модели без Unity API  
- **Application** – сценарии использования (UseCases), правила и сервисы  
- **Presentation** – UI + Presenters (MVP-подход)  
- **Infrastructure** – интеграция с Unity, DI, сериализация  

---

## ✅ Реализовано
- Базовый UI Toolkit: панель ресурсов, кнопки для постройки зданий  
- 3 типа зданий (Дом, Ферма, Шахта)  
- UseCases: установка, перемещение, удаление, апгрейд зданий  
- Экономика: начисление Gold от зданий каждые N секунд  
- DI через VContainer  
- События через MessagePipe  
- Тесты для Domain (стоимость, апгрейд, проверка занятости клетки)  

---

## ❌ Не успел реализовать
- Система сохранения/загрузки состояния города (JSON)  
- Подсветка клетки grid под курсором (зелёная/красная область)
- Система вращения/перемещения зданий

---

## ▶️ Запуск
1. Открыть проект в **Unity 6**  
2. Открыть сцену `SampleScene`  
3. Запустить Play → можно ставить здания, апгрейдить, получать прибыль  
