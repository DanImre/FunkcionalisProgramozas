module Lesson5 where
import Data.List
import Data.Char

polinom :: Num a => [a] -> a -> a
polinom [] x = 0
polinom (z:zs) x = z * x^((length zs)) + (polinom zs x)

runs :: Int -> [a] -> [[a]]
runs x y
    | length y == 0 = []
    | length y < x = [y]
    | otherwise = [take x y] ++ (runs x (drop x y))

every :: Int -> [a] -> [a]
every _ [] = []
--every x y = [head(take x y)] ++ every x (drop x y) --Ez is jó
every x yys@(y:ys) = y : every x (drop x yys) --Legegyszerűbb

--n'2 futásidő
reverse' :: [a] -> [a]
reverse' [] = []
reverse' (x:xs) = (reverse' xs) ++ [x]

--n (lineáris) futási idő
--végrekutzió - tail recursion
reverse'' :: [a] -> [a]
reverse'' l = rev l [] where
    rev :: [a] -> [a]-> [a]
    rev [] yys = yys
    rev (x:xs) yys = rev xs (x:yys) --rekurzio a legkülső


upperLower :: Char -> Char
upperLower c
    | isUpper c = toLower c
    | otherwise = toUpper c --otherwise egy konstants true értékű Bool 