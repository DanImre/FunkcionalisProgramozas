module Zarthelyi1 where

--1. kérdés : Egy olyan függvény ami nem kezel minden esetet.

--2. kérdés : A bekért adat "Int" tipúsú kell legyen, ezért össze lehet adni az 5 el.

--Legyen világosság
brighten :: (Int, Int, Int) -> Int -> (Int, Int, Int)
brighten (a,b,c) d = (a + d, b + d, c + d)

--Ésből következik
andImpl :: Bool -> Bool -> Bool -> Bool
andImpl True True False = False
andImpl _ _ _ = True

--Szelektív inverzió
selectiveInversion :: [Int] -> [Int]
selectiveInversion a = [-n | n <- a, n `mod` 7 == 0, not(n `mod` 2 == 0)]