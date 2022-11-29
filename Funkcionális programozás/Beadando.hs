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
performZombieActionsSeged :: [(Coordinate, Plant)] -> Coordinate -> [(Coordinate, Plant)]

performZombieActions :: GameModel -> Maybe GameModel
performZombieActions (GameModel sun plantMap (((x,y),zombie):zombieMap))
    belsoseged :: 
    | (y == 0) = Nothing
    | (lookup (x,y) plantMap == Nothing) = 

 -- = (GameModel sun plantMap (map performZombieActionsSeged zombieMap))