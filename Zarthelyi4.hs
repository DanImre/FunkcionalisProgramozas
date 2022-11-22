module Zarthelyi4 where
import Data.List
import Data.Char

--Miért nem értelmezhető a következő kifejezés és hogyan tehető értelmezhetővé? Car == Bus
--Mert nem az Eq osztály része.


data Computer = Laptop String Int Int Int Bool | PC String Int Int Bool
    deriving (Eq, Show)

--PC :: String -> Int -> Int -> Bool -> Computer
--Laptop :: String -> Int -> Int -> Int -> Bool -> Computer

type ComputerRoom = [Computer]

getCpuMan :: Computer -> String
getCpuMan (Laptop x _ _ _ _) = x
getCpuMan (PC x _ _ _) = x

sumCapacity :: ComputerRoom -> Int
sumCapacity (Laptop _ _ x _ _:xs) = x + sumCapacity xs
sumCapacity (PC _ _ x _:xs) = x + sumCapacity xs
sumCapacity _ = 0

turnOffForService :: ComputerRoom -> ComputerRoom
turnOffForService ((PC x y z d):xs)
    | (x == "AMD" && y < 4096) = (PC x y z False) : turnOffForService xs
    | otherwise = (PC x y z d) : turnOffForService xs
turnOffForService (x:xs) = x : turnOffForService xs
turnOffForService [] = []