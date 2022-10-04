module Lesson4 where

--faktorialis
fact :: Integer -> Integer
fact 0 = 1
fact a = a * fact (a - 1)

fib :: Integer -> Integer
fib 0 = 0
fib 1 = 1
fib a = fib (a - 1) + fib(a - 2)

length' :: [a] -> Int
length' [] = 0
length' (_:as) = 1 + length' as

sum' :: Num a => [a] -> a
sum' [] = 0
sum' (a:as) = a + sum'(as)

product' :: Num a => [a] -> a
product' [] = 1
product' (a:as) = a * sum'(as)

last' :: Num a => [a] -> a
last' [x] = x
last' (a:as) = last' as

init' :: [a] -> [a]
init' [a] = []
init' (a:as) = a:init' as

minimum' :: Ord a => [a] -> a --Rendezhetőnek kell lennie a típusnak
minimum' [a] = a
minimum' (a:as) = a `min` minimum' as

zip' :: [a] -> [b] -> [(a,b)]
zip' (a:as) [b] = [(a,b)]
zip' [a] (b:bs) = [(a,b)]
zip' (a:as) (b:bs) = (a,b):zip as bs