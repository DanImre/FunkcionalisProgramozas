module Zarthelyi2 where
import Data.List
import Data.Char

--Mi a magasabbrendű függvény? Egy olyan függvény amely paraméterként vár egy függvényt vagy visszaad egyet

funAnd :: ((a -> Bool) , (b -> Bool)) -> (a, b) -> Bool
funAnd (f,g) (a, b) = (f a && g b)

conditionalMap :: (a -> a) -> (a -> Bool) -> [a] -> [a]
conditionalMap f g (x:xs)
    | (g x) = (f x) : conditionalMap f g xs
    | otherwise = x : conditionalMap f g xs
conditionalMap _ _ _ = []

highRemainderMaxSeged :: Integral a => [a] -> a
highRemainderMaxSeged (x:xs)
    | (x > highRemainderMaxSeged xs) = x
    | otherwise = highRemainderMaxSeged xs
highRemainderMaxSeged _ = 0

highRemainder :: Integral a => a -> [a] -> a
highRemainder a (x:xs) = highRemainderMaxSeged([x `mod` a] ++ [highRemainder a xs] )
highRemainder _ _ = 0