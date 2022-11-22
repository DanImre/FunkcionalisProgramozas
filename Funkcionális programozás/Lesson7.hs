module Lesson7 where
import Data.List
import Data.Char

map :: (a -> b) -> [a] -> [b]
map f l = [ f n | n <- l]

filter :: (a -> Bool) -> [a] -> [a]
filter f l = [ n | n <- l , f n]

count :: (a -> Bool) -> [a] -> Int
count f l = length ( filter f l)

takeWhile :: (a -> Bool) -> [a] -> [a]
takeWhile _ [] = []
takeWhile f (x:xs)
    | (f x) = x : takeWhile f xs
    | otherwise = []

dropWhile :: (a -> Bool) -> [a] -> [a]
dropWhile _ [] = []
dropWhile f (x:xs)
    | (f x) = dropWhile f xs
    | otherwise = x : xs

span :: (a -> Bool) -> [a]{-véges-} -> ([a],[a])
span _ [] = ([],[])
span p (x:xs) | ( p x ) = (x : a, b) where
        (a, b) = span p xs -- mintaillesztés az eredménye
span p (x:xs) = ([],x:xs)

iterate :: (a -> a) -> a -> [a]  
iterate p x = [x] ++ iterate p (p x)

infixr 0 $
($) :: (a -> b) -> a -> b
--fingom sincs

all :: (a -> Bool) -> [a]{-véges-} -> Bool
all _ [] = True
all p (x:xs)
    | (p x) = all p xs
    | otherwise = False

any :: (a -> Bool) -> [a]{-véges-} -> Bool
any _ [] = False
any p (x:xs)
    | (p x) = True
    | otherwise = any p xs 


fibPairs :: [(Integer, Integer)]
fibPairs = iterate (\(a, b) -> (b, a + b)) (0,1)

{-
group :: Eq a => [a]{-véges-} -> [[a]]
group _ [] = []
group l -}