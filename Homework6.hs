module Homework6 where
import Data.Char
import Data.List

hasFever :: Int -> Bool
hasFever a = ( fromIntegral(a - 32) / 1.8 ) > 38

positiveProduct :: (Num a, Ord a) => [a] -> a
positiveProduct [] = 1
positiveProduct (x:xs)
    | (x < 0) = positiveProduct xs
    | otherwise = x * positiveProduct xs

mightyGale :: [(String, Int, Int, Int)] -> String
mightyGale [] = ""
mightyGale ((a, b, c, d):xs)
    | (c > 110) = a
    | otherwise = mightyGale xs

cipher :: String -> String
cipher (a:b:c:xs)
    | ((not (isDigit a)) && (not (isDigit b)) && (isDigit c)) = [a,b]
    | otherwise = cipher ([b,c] ++ xs)
cipher _ = ""

pizza :: [(String, Int)] -> Int
pizza [] = 500
pizza ((a,b):xs) = b + pizza xs

contains :: Eq a => a -> [a] -> Bool
contains _ [] = False
contains b (x:xs)
    | (b == x) = True
    | otherwise = contains b xs

listDiff :: Eq a => [a] -> [a] -> [a]
listDiff [] _ = []
listDiff (x:xs) b
    | (contains x b) = listDiff xs b
    | otherwise = x : listDiff xs b

validGameSeged :: [String] -> Bool
validGameSeged (a:b:xs) = (last a == head b) && (validGameSeged (b:xs))
validGameSeged _ = True

validGame :: String -> Bool
validGame a = validGameSeged ( words a ) 