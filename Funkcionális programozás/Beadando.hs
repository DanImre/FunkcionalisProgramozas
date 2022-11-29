module Beadando where
import Data.List
import Data.Char

defaultPeashooter :: Plant
defaultPeashooter = Peashooter 3

defaultSunflower :: Plant
defaultSunflower = Sunflower 2

defaultWalnut :: Plant
defaultWalnut = Walnut 15

defaultCherryBomb :: Plant
defaultCherryBomb = CherryBomb 2

basic :: Zombie
basic = Basic 5 1

coneHead :: Zombie
coneHead = Conehead 10 1

bucketHead :: Zombie
bucketHead = Buckethead 20 1

vaulting :: Zombie
vaulting = Vaulting 7 2

type Coordinate = (Int, Int)
type Sun = Int

data Plant = Peashooter Int | Sunflower Int | Walnut Int | CherryBomb Int
    deriving (Show,Eq)

data Zombie = Basic Int Int | Conehead Int Int | Buckethead Int Int | Vaulting Int Int
    deriving (Show,Eq)

data GameModel = GameModel Sun [(Coordinate, Plant)] [(Coordinate, Zombie)]
    deriving (Show,Eq)

tryPurchaseSegedEgy :: Sun -> Plant -> Bool
tryPurchaseSegedEgy x (Peashooter _) = (x >= 100)
tryPurchaseSegedEgy x (CherryBomb _) = (x >= 150)
tryPurchaseSegedEgy x _ = (x >= 50)

tryPurchaseSegedKetto :: Plant -> Sun
tryPurchaseSegedKetto (Peashooter _) = 100
tryPurchaseSegedKetto (CherryBomb _) = 150
tryPurchaseSegedKetto _ = 50

tryPurchase :: GameModel -> Coordinate -> Plant -> Maybe GameModel
tryPurchase (GameModel sun plantMap zombieMap) (x,y) plant
    | (x >= 0 && x < 5 && y <= 0 && y < 12 && lookup (x,y) plantMap == Nothing && tryPurchaseSegedEgy sun plant) = Just (GameModel (sun - (tryPurchaseSegedKetto plant))  (((x,y), plant) : plantMap) zombieMap)
    | otherwise = Nothing

placeZombieInLane :: GameModel -> Zombie -> Int -> Maybe GameModel
placeZombieInLane (GameModel sun plantMap zombieMap) zombie sav
    | (sav >= 0 && sav < 5 && lookup (sav, 11) zombieMap == Nothing) = Just (GameModel sun plantMap (((sav, 11), zombie): zombieMap))
    | otherwise = Nothing

--csökkenti a növény életét
performZombieActionsSeged :: [(Coordinate, Plant)] -> [(Coordinate, Zombie)] -> [(Coordinate, Plant)]
performZombieActionsSeged (((xP,yP), Peashooter x) : xs) zombieMap
    | (lookup (xP, yP) zombieMap == Nothing) = ((xP,yP), Peashooter x) : performZombieActionsSeged xs zombieMap
    | otherwise = ((xP,yP), (Peashooter (x - 1))) : performZombieActionsSeged xs zombieMap
performZombieActionsSeged (((xP,yP), Sunflower x) : xs) zombieMap
    | (lookup (xP, yP) zombieMap == Nothing) = ((xP,yP), Sunflower x) : performZombieActionsSeged xs zombieMap
    | otherwise = ((xP,yP), (Sunflower (x - 1))) : performZombieActionsSeged xs zombieMap
performZombieActionsSeged (((xP,yP), Walnut x) : xs) zombieMap
    | (lookup (xP, yP) zombieMap == Nothing) = ((xP,yP), Walnut x) : performZombieActionsSeged xs zombieMap
    | otherwise = ((xP,yP), (Walnut (x - 1))) : performZombieActionsSeged xs zombieMap
performZombieActionsSeged (((xP,yP), CherryBomb x) : xs) zombieMap
    | (lookup (xP, yP) zombieMap == Nothing) = ((xP,yP), CherryBomb x) : performZombieActionsSeged xs zombieMap
    | otherwise = ((xP,yP), (CherryBomb (x - 1))) : performZombieActionsSeged xs zombieMap
performZombieActionsSeged [] _ = []

performZombieActions :: GameModel -> Maybe GameModel
performZombieActions (GameModel sun plantMap zombieMap)
    | (length (filter (\((xB,yB), zombieB) -> yB <= 0) zombieMap) > 0) = Nothing
    | otherwise = Just (GameModel sun (performZombieActionsSeged plantMap (belsoseged zombieMap)) (belsoseged zombieMap)) where
        belsoseged :: [(Coordinate, Zombie)] -> [(Coordinate, Zombie)]
        belsoseged (((xC,yC), Vaulting z 2) : xs)
            | (lookup (xC,yC) plantMap == Nothing && yC == 1) = ((xC,yC - 1), Vaulting z 2) : belsoseged xs
            | (lookup (xC,yC) plantMap == Nothing) = ((xC,yC - 2), Vaulting z 2) : belsoseged xs
            | (yC == 1) = ((xC,yC - 1), Vaulting z 1) : belsoseged xs
            | otherwise = ((xC,yC - 2), Vaulting z 1) : belsoseged xs
        belsoseged (((xC,yC), (Basic z x)) : xs)
            | ((lookup (xC,yC) plantMap == Nothing)) = ((xC,yC - x), (Basic z x)) : belsoseged xs
            | otherwise = ((xC,yC), (Basic z x)) : belsoseged xs
        belsoseged (((xC,yC), (Conehead z x)) : xs)
            | ((lookup (xC,yC) plantMap == Nothing)) = ((xC,yC - x), (Conehead z x)) : belsoseged xs
            | otherwise = ((xC,yC), (Conehead z x)) : belsoseged xs
        belsoseged (((xC,yC), (Buckethead z x)) : xs)
            | ((lookup (xC,yC) plantMap == Nothing)) = ((xC,yC - x), (Buckethead z x)) : belsoseged xs
            | otherwise = ((xC,yC), (Buckethead z x)) : belsoseged xs
        belsoseged [] = []

cleanBoard :: GameModel -> GameModel
cleanBoard (GameModel sun plantMap zombieMap) = (GameModel sun (plantCleaner plantMap) (zombieCleaner zombieMap)) where
    plantCleaner :: [(Coordinate, Plant)] -> [(Coordinate, Plant)]
    plantCleaner (((x,y), Peashooter 0) : xs) = plantCleaner xs
    plantCleaner (((x,y), Sunflower 0) : xs) = plantCleaner xs
    plantCleaner (((x,y), Walnut 0) : xs) = plantCleaner xs
    plantCleaner (((x,y), CherryBomb 0) : xs) = plantCleaner xs
    plantCleaner (x:xs) = x : plantCleaner xs
    plantCleaner [] = []
    zombieCleaner :: [(Coordinate, Zombie)] -> [(Coordinate, Zombie)]
    zombieCleaner (((x,y), Basic 0 _) : xs) = zombieCleaner xs
    zombieCleaner (((x,y), Conehead 0 _) : xs) = zombieCleaner xs
    zombieCleaner (((x,y), Buckethead 0 _) : xs) = zombieCleaner xs
    zombieCleaner (((x,y), Vaulting 0 _) : xs) = zombieCleaner xs
    zombieCleaner (x:xs) = x : zombieCleaner xs
    zombieCleaner [] = []

--Opcionális rész