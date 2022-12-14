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
    | (x >= 0 && x < 5 && y >= 0 && y < 12 && lookup (x,y) plantMap == Nothing && tryPurchaseSegedEgy sun plant) = Just (GameModel (sun - (tryPurchaseSegedKetto plant))  (((x,y), plant) : plantMap) zombieMap)
    | otherwise = Nothing

placeZombieInLane :: GameModel -> Zombie -> Int -> Maybe GameModel
placeZombieInLane (GameModel sun plantMap zombieMap) zombie sav
    | (sav >= 0 && sav < 5 && lookup (sav, 11) zombieMap == Nothing) = Just (GameModel sun plantMap (((sav, 11), zombie): zombieMap))
    | otherwise = Nothing

performZombieActions :: GameModel -> Maybe GameModel
performZombieActions (GameModel sun plants zombies)
    | (any (\((x,y),zombie) -> y == 0) zombies) = Nothing
    | otherwise = Just (GameModel sun (hitPlants zombies plants) (changeZombiePosition plants zombies))


hitPlants :: [(Coordinate, Zombie)] -> [(Coordinate, Plant)] -> [(Coordinate, Plant)]
hitPlants [] plantMap = plantMap
hitPlants ((coord, zombie):zombies) plantMap
    | Basic x y <- zombie = hitPlants zombies (hitPlantAt coord plantMap) 
    | Conehead x y <- zombie = hitPlants zombies (hitPlantAt coord plantMap)
    | Buckethead x y <- zombie = hitPlants zombies (hitPlantAt coord plantMap)
    | Vaulting x 1 <- zombie = hitPlants zombies (hitPlantAt coord plantMap)
    | Vaulting x 2 <- zombie = hitPlants zombies plantMap

hitPlantAt :: Coordinate -> [(Coordinate, Plant)] -> [(Coordinate, Plant)]
hitPlantAt coord [] = []
hitPlantAt coord ((coord2, plant):plants)
    | coord == coord2 = ((coord2, hitPlantAtSeged plant):hitPlantAt coord plants)
    | otherwise = ((coord2, plant):hitPlantAt coord plants)

hitPlantAtSeged :: Plant -> Plant
hitPlantAtSeged (Peashooter health) = Peashooter (health-1)
hitPlantAtSeged (Sunflower health) = Sunflower (health-1)
hitPlantAtSeged (Walnut health) = Walnut (health-1)
hitPlantAtSeged (CherryBomb health) = CherryBomb (health-1)

changeZombiePosition :: [(Coordinate, Plant)] -> [(Coordinate, Zombie)] -> [(Coordinate, Zombie)]
changeZombiePosition plantMap zombiMap = map (\(coord, zombie) -> 
    if (isVaultingWith2Speed zombie) 
        then 
            if isThereAPlantHere (fst coord, snd coord) plantMap 
                then ((jumpShort (fst coord, snd coord)), Vaulting (getZombieHealth zombie) 1)
            else if isThereAPlantHere (fst coord, snd coord-1) plantMap 
                then ((jumpLong (fst coord, snd coord)), Vaulting (getZombieHealth zombie) 1)
            else 
                (jumpLong(fst coord, snd coord), zombie)
    else if isThereAPlantHere coord plantMap
        then (coord, zombie) 
    else ((fst coord, snd coord - 1), zombie)) zombiMap

isVaultingWith2Speed :: Zombie -> Bool
isVaultingWith2Speed (Vaulting _ 2) = True
isVaultingWith2Speed _ = False

jumpShort :: Coordinate -> Coordinate
jumpShort (x,y) 
    | y-1 <= 0 = (x,0)
    | otherwise = (x,y-1)

jumpLong :: Coordinate -> Coordinate
jumpLong (x,y) 
    | y-2 <= 0 = (x,0)
    | otherwise = (x,y-2)

getZombieHealth :: Zombie -> Int
getZombieHealth (Vaulting x _) = x
getZombieHealth (Basic x _) = x
getZombieHealth (Conehead x _) = x
getZombieHealth (Buckethead x _) = x

isThereAPlantHere :: Coordinate -> [(Coordinate, Plant)] -> Bool
isThereAPlantHere coord [] = False
isThereAPlantHere coord ((coord2, plant) : plants)
    | (coord == coord2) = True
    | otherwise = (isThereAPlantHere coord plants)

{-
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
    zombieCleaner [] = []-}

cleanBoard :: GameModel -> GameModel
cleanBoard (GameModel sun plantMap zombieMap) = (GameModel sun (filter (\(_ , plant) -> getPlantHealth plant > 0) plantMap) (filter (\(_ , zombie) -> getZombieHealth zombie > 0) zombieMap)) where

getPlantHealth :: Plant -> Int
getPlantHealth (Peashooter x) = x
getPlantHealth (Sunflower x) = x
getPlantHealth (Walnut x) = x
getPlantHealth( CherryBomb x) = x

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