module Lesson1 where

one :: Int
two :: Int

one = 1
two = 1 + one

inc :: Int -> Int
inc a = a + 1

inc' :: Int -> Int
inc' a = (+) a 1

add :: Int -> Int -> Int
add a b = a + b


letterE :: Char
letterE = 'E'

isEven :: Int -> Bool
isEven a = a `mod` 2 == 0

isOdd :: Int -> Bool
isOdd a = a `mod` 2 /= 0

isOdd' :: Int -> Bool
isOdd' a = not (isEven a)


returnFirst :: a -> b -> a --nem muszáj a 2 nek ugyanazon tipúsúnak lenni
returnFirst a b = a

returnFirst' :: a -> a -> a --Itt muszáj a 2 nek ugyanazon tipúsúnak lenni
returnFirst' a b = a

greater :: Int -> Int -> Bool
greater a b = a > b
