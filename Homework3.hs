module Homework3 where

--Lista konstrukció

putIntoList :: a -> [a]
putIntoList b = [b]

interval :: Int -> Int -> [Int]
interval a b = [a..b]

--Lista dekonstrukció

headTail :: [a] -> (a, [a])
headTail (x:xs) = (x,xs)

doubleHead :: [a] -> [b] -> (a, b)
doubleHead (a:as) (b:bs) = (a,b)

--Halmazkifejezések

hasZero :: [Int] -> Bool
hasZero [] = False
hasZero (x:xs)
    | (x == 0) = True
    | otherwise = hasZero xs

hasEmpty :: [[a]] -> Bool
hasEmpty [] = False
hasEmpty (x:xs) 
    | (null x) = True
    | otherwise = hasEmpty xs

doubleAll :: [Int] -> [Int]
doubleAll a = [ n*2 | n <- a]

--ezekszerint ez is kell
isLonger :: [a] -> [b] -> Bool
isLonger a [] = True
isLonger [] b = False
isLonger (a:as) (b:bs) = isLonger as bs