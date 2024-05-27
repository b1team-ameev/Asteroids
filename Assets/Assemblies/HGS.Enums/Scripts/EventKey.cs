namespace HGS.Enums {
    
    public enum EventKey {
        
        Unknown = 0,

        OnServiceInitialized = 100, // сервис инициализирован  
        OnRemoteConfigInitialized = 110, // удаленная конфигация инициализирована

        OnFirebaseInitialized = 200, // Firebase инициализирован
        OnUnityStoreInitialized = 210, // магазин Unity инициализирован
        OnRuStoreInitialized = 220, // магазин RuStore инициализирован

        OnGameChangeControlsState = 400, // игра сообщает о необходимости/показать скрыть управление
        OnGameChangePauseState = 410, // игра сообщает о паузе

        OnNewGame = 500, // новая игра
        OnGameFinish = 510, // игра закончена
        OnGameLosed = 520, // игра проиграна

        OnSceneLoaded = 600, // загрузка сцены
        OnNeedRestartScene = 620, // необходимо перезагрузить текущую сцену

        OnAppPause = 700, // пауза приложения
        OnAppError = 710, // ошибка приложения
        OnAppExit = 730, // выход из приложения
        OnAppInitialized = 720, // приложение инициализировано

        OnGoodsPaid = 900, // был оплачен товар
        OnRestorePurchases = 910, // высстановление покупок

        OnShowDataObject = 1000, // вывод некого объекта на экране

        OnAppGoToBackState = 1200, // возврат к предыдущему состоянию
        OnAppGoToAfterGameState = 1210, // переход к состоянию после окончания игры

        // #AsteroidsGame
        
        OnAppGoToMainMenuState = 1220, // к главному меню
        OnAppGoToGameState = 1230, // начать игру
        
        OnSpaceshipDestroyed = 1500, // корабль был уничтожен
        OnAsteroidDestroyed = 1510, // астероид был уничтожен
        OnAsteroidSpawned = 1520, // астероид был создан
        
        OnNewWave = 1600, // новая волна
        OnNeedSpawnUfo = 1610, // требуется заспавнить НЛО

        OnShowScore = 1700, // вывод счета на экран
        
        OnShowCoordinates = 1800, // вывод координат 
        OnShowRotationAngle = 1810, // вывод угол поворота 
        OnShowInstantSpeed = 1820, // вывод мгновенной скорости 
        
        OnShowTimeBeforeReload = 1830, // вывод времени до перезагрузки
        OnShowBulletCount = 1840, // вывод количества зарядов
        

    }

}