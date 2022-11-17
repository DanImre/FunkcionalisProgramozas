module Homework8 where
import Data.Char
import Data.List
--import Data.Text

contains :: [Char] -> Char -> Bool
contains [] _ = False
contains (x:xs) a
    | (x == a) = True
    | otherwise = contains xs a

which :: ([Char], [Char], [Char]) -> Char -> Int
which (a,b,c) x
    | (contains a x) = 1
    | (contains b x) = 2
    | (contains c x) = 3
    | otherwise = 0


numeric :: String -> Int
numeric [] = 0
numeric (x:xs)
    | (x == 'r') = 4 + numeric xs
    | (x == 'w') = 2 + numeric xs
    | otherwise = 1 + numeric xs


pythagoreans :: [(Int, Int, Int)]
pythagoreans = [ (a,b,c) | a <- [1..100], b <- [1..100], c <- [1..100] , a < b, (a^2)+(b^2) == (c^2)]

isLonger :: [a] -> Int -> Bool
isLonger (x:xs) n
    | (n == 0) = True
    | otherwise = isLonger xs (n - 1)
isLonger [] n = False

removeAccents :: String -> String
removeAccents [] = []
removeAccents (x:xs)
    | (x == 'á') = 'a' : removeAccents xs
    | (x == 'é') = 'e' : removeAccents xs
    | (x == 'í') = 'i' : removeAccents xs
    | (x == 'ó') = 'o' : removeAccents xs
    | (x == 'ö') = 'o' : removeAccents xs
    | (x == 'ő') = 'o' : removeAccents xs
    | (x == 'ú') = 'u' : removeAccents xs
    | (x == 'ü') = 'u' : removeAccents xs
    | (x == 'ű') = 'u' : removeAccents xs
    | otherwise = x : removeAccents xs

strip :: String -> String
strip x = dropWhile (== '_') (reverse (dropWhile (== '_') (reverse x)))