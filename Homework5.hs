module Homework5 where

geometricSequence :: Num a => a -> a -> [a]
geometricSequence a b = a : geometricSequence (a * b) b

isSorted :: Ord a => [a] -> Bool
isSorted (x:y:xs) = (x < y) && isSorted xs
isSorted _ = True

fromTo :: Int -> Int -> [a] -> [a]
fromTo a b c = drop a (take b c)

{-
contains' :: Eq a => a -> [a] -> Bool
contains' _ [] = False
contains' a [b] = a == b
contains' a (b:bs)
    | (a == b) = True
    | otherwise = contains' a bs
-}

megMeddigUgyanOlyanok :: Eq a => a -> [a] -> Int
megMeddigUgyanOlyanok a (b:bs)
    | (b == a) = 1 + megMeddigUgyanOlyanok a bs
    | otherwise = 0;
megMeddigUgyanOlyanok _ _ = 0;

uniques :: Eq a => [a] -> [a]
uniques (a:as) = a : uniques(drop (megMeddigUgyanOlyanok a as) as)
uniques a = a