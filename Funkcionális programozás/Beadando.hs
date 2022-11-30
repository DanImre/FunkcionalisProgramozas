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

isSunflower :: Plant -> Bool
isSunflower (Sunflower _) = True
isSunflower _ = False

lowerHealth :: Zombie -> Zombie
lowerHealth (Basic x y) = Basic (x-1) y
lowerHealth (Conehead x y) = Conehead (x-1) y
lowerHealth (Buckethead x y) = Buckethead (x-1) y
lowerHealth (Vaulting x y) = Vaulting (x-1) y

killZombie :: Zombie -> Zombie
killZombie (Basic x y) = Basic 0 y
killZombie (Conehead x y) = Conehead 0 y
killZombie (Buckethead x y) = Buckethead 0 y
killZombie (Vaulting x y) = Vaulting 0 y

plantMapCherryBombs :: [(Coordinate, Plant)] -> [(Coordinate, Plant)]
plantMapCherryBombs ((cord, CherryBomb x) : xs) = (cord, CherryBomb 0) : plantMapCherryBombs xs
plantMapCherryBombs (x:xs) = x : plantMapCherryBombs xs
plantMapCherryBombs [] = []



--(sortBy (\((xF,yF), zombie) -> xF) (filter (\((xF,yF), zombie) -> xF == x) zombieMap))

zombieMapAlakitas :: [(Coordinate, Plant)] -> [(Coordinate, Zombie)] -> [(Coordinate, Zombie)]
zombieMapAlakitas (((x,y), Peashooter z) : xs) zombieMap
    | ((coordinateOfTheFirstZombieInTheRow (filter (\((xZ,yZ), zombie) -> yZ >= y) zombieMap) x) < 13) = zombieMapAlakitas xs (damageZombieAtCords (x, (coordinateOfTheFirstZombieInTheRow (filter (\((xZ,yZ), zombie) -> yZ >= y) zombieMap) x)) zombieMap)
    | otherwise = zombieMapAlakitas xs zombieMap
zombieMapAlakitas (((x,y), CherryBomb z) : xs) zombieMap = (killZombieAtCords (x + 1,y + 1) (killZombieAtCords (x - 1,y + 1) (killZombieAtCords (x + 1,y - 1) (killZombieAtCords (x - 1,y - 1) (killZombieAtCords (x ,y + 1) (killZombieAtCords (x ,y - 1) (killZombieAtCords (x + 1,y) (killZombieAtCords (x - 1 ,y) (killZombieAtCords (x,y) zombieMap)))))))))
zombieMapAlakitas _ z = z

coordinateOfTheFirstZombieInTheRow :: [(Coordinate, Zombie)] -> Int -> Int
coordinateOfTheFirstZombieInTheRow (((x,y), zombie) : xs) row
    | (x == row && y <= (coordinateOfTheFirstZombieInTheRow xs row)) = y
    | otherwise = coordinateOfTheFirstZombieInTheRow xs row
coordinateOfTheFirstZombieInTheRow [] _ = 13

damageZombieAtCords :: Coordinate -> [(Coordinate, Zombie)] -> [(Coordinate, Zombie)]
damageZombieAtCords (x,y) (((xZ,yZ), zombie) : xs)
    | (x == xZ && y == yZ) = ((xZ,yZ), (lowerHealth zombie)) : xs
    | otherwise = ((xZ,yZ), zombie) : damageZombieAtCords (x,y) xs
damageZombieAtCords _ [] = []

killZombieAtCords :: Coordinate -> [(Coordinate, Zombie)] -> [(Coordinate, Zombie)]
killZombieAtCords (x,y) (((xZ,yZ), zombie) : xs)
    | (x == xZ && y == yZ) = ((xZ,yZ), (killZombie zombie)) : xs
    | otherwise = ((xZ,yZ), zombie) : killZombieAtCords (x,y) xs
killZombieAtCords _ [] = []

performPlantActions :: GameModel -> GameModel
performPlantActions (GameModel sun plantMap zombieMap) = (GameModel (sun + length (filter (\((x,y), plant) -> isSunflower plant) plantMap) * 25) (plantMapCherryBombs plantMap) (zombieMapAlakitas plantMap zombieMap))

fromMaybe :: Maybe a -> a
fromMaybe (Just x) = x
fromMaybe _ = error "Nothing volt"

--createNormalZombieMap :: [[(Int, Zombie)]] -> [(Coordinate, Zombie)]
--map (\(x, zombie) -> ((x,11), zombie)) z

defendsAgainstSeged :: GameModel -> [[(Int, Zombie)]] -> Int -> Bool
defendsAgainstSeged (GameModel sun plantMap zombieMap) (z : zs) kor
    | (kor == 1) = defendsAgainstSeged (GameModel sun plantMap zombieMap) (z:zs) (kor + 1)
    | (kor == 2) = defendsAgainstSeged (performPlantActions (GameModel sun plantMap zombieMap)) (z:zs) (kor + 1)
    | (kor == 3) = defendsAgainstSeged (cleanBoard (GameModel sun plantMap zombieMap)) (z:zs) (kor + 1)
    | (kor == 4 && (performZombieActions (GameModel sun plantMap zombieMap)) == Nothing) = False
    | (kor == 4) = defendsAgainstSeged (fromMaybe (performZombieActions (GameModel sun plantMap zombieMap))) (z:zs) (kor + 1)
    | (kor == 5) = defendsAgainstSeged (GameModel sun plantMap (zombieMap ++ (map (\(x, zombie) -> ((x,11), zombie)) z))) zs (kor + 1)
    | (kor == 6) = defendsAgainstSeged (cleanBoard (GameModel sun plantMap zombieMap)) (z:zs) (kor + 1)
    | (kor == 7) = defendsAgainstSeged (GameModel (sun + 25) plantMap zombieMap) (z:zs) 1
defendsAgainstSeged _ [] _ = True

defendsAgainst :: GameModel -> [[(Int, Zombie)]] -> Bool
defendsAgainst (GameModel s p zM) z = defendsAgainstSeged (GameModel s p zM) z 1