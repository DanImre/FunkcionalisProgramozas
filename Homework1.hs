module Homework1 where

intExpr1 :: Int
intExpr2 :: Int
intExpr3 :: Int
intExpr1 = 1
intExpr2 = 1 + 1
intExpr3 = 4 - 1

charExpr1 :: Char
charExpr2 :: Char
charExpr3 :: Char
charExpr1 = 'A'
charExpr2 = 'B'
charExpr3 = 'C'

boolExpr1 :: Bool
boolExpr2 :: Bool
boolExpr3 :: Bool
boolExpr1 = False
boolExpr2 = True
boolExpr3 = not True


--Virágültetés (bekérés nélkül)
canPlantAll :: Bool
canPlantAll = (183 `mod` 13) == 0

remainingSeeds :: Int
remainingSeeds = (183 `mod` 13)

--Hét
inc :: Int -> Int
inc a = a + 1
double :: Int -> Int
double a = 2*a

seven1 :: Int
seven1 = inc(double(inc(inc(inc(0)))))
seven2 :: Int
seven2 = inc(double(inc(double(inc(0)))))
seven3 = inc(inc(inc(double(double(inc(0))))))

cmpRem5Rem7 :: Int -> Bool
cmpRem5Rem7 a = (a `mod` 5) > (a `mod` 7)

--Típusszignatúra
foo :: Int -> Bool -> Bool
foo a b = True
bar :: Bool -> Int -> Bool
bar a b = foo b a

