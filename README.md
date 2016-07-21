﻿# Практика работы с DI-контейнером

1. Разминка. В классе Program переделайте Main так, чтобы MainForm создавался контейнером.
Удалите у MainForm конструктор без параметров и сделайте так, чтобы контейнер инжектировал в MainForm список IUiAction.

2. INeed<T>. Изучите код KochFractalAction. Изучите механику работы INeed<T> и DependencyInjector.
Оцените такой подход к управлению зависимостями.

3. Рефакторинг. Измените класс KochFractalAction так, чтобы все его зависимости инжектировались явно через конструктор, 
без использования интерфейса INeed.

4. Фабрика. Аналогично удалите INeed из класса DragonFractalAction.
Особенность в том, что одна из зависимостей DragonPainter — DragonSettings оказывается известной только в процессе работы экшена.
Используйте возможности инжектирования фабрик, чтобы справится с этой задачей. https://github.com/ninject/Ninject.Extensions.Factory/wiki

5. Новая зависимость. Переведите DragonPainter на использование цветов палитры, как это сделано в KochPainter.
Заметьте, что не пришлось ничего доконфигурировать — только добавить параметр в конструктор.

6. Источник зависимости. Аналогично отрефакторите ImageSettingsAction.
Попробуйте придумать, как сделать так, чтобы ImageSettingsAction принимал в качестве зависимости не IImageSettingsProvider, 
а сам ImageSettings.

7. Избавьтесь от остальных использований INeed и удалите этот интерфейс и класс DependencyInjector из проекта.

8. При создании диалогового окна рекомендуется устанавливать у него в качестве владельца основное окно программы. 
Подумайте, как это можно сделать?

9. SettingsForm и SaveFileDialog — зависимости, которые не инжектируются. Как можно изменить код, чтобы эти зависимости также инжектировались 
контейнером. Зачем это могло бы пригодиться?

10. Можно ли как-то протестировать корректность конфигурирования контейнера?