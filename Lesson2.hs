module Lesson2 where

inc :: Int -> Int
inc a = a + 1;

exp, exp1, exp2 :: Int
exp = (inc. inc . inc . inc) 1
exp1 = inc(inc(inc(inc(1))))
exp2 = inc $ inc $ inc $ inc 1

--for haskellül: Az én faszságaim

--Tippek : Fontos a hely, pontosan így kell elválasztani
{-
a :: Int
a = 1

loop :: Int -> Int
loop n = loop' n 1
    where loop' 0 a = a
          loop' n a = loop' (n - 1) (a * 2)
-}
m :: Int
m = 1

nToThePowerOf2 :: Int -> Int
nToThePowerOf2 n = loopMultiplication n 1
    where loopMultiplication 0 m = m
          loopMultiplication n m = loopMultiplication (n - 1) (m * 2)

--polimorfikus 
returnFirst :: a -> b -> a
returnFirst a b = a

--"megszorítás"
inc' :: Num a => a -> a --rögzíti az a tipusát
inc' a = a + 1

mySpecialAdd :: Int -> Integer -> Integer
mySpecialAdd a b = fromIntegral a + b

{- 

Matekosparancsok:

truncate "levágja a vesszőt"
floor
cieling
round


-}

--Feladatok:

--MintaIllesztés
not' :: Bool -> Bool
not' True = False
not' False = True

(&&&) :: Bool -> Bool-> Bool
--(&&&) a b = (a == b) && a == True
(&&&) True True = True
--(&&&) a b = False
(&&&) _ _ = False --"Joker karakter" - han em használjuk a változókat

egyenloOttel :: Int -> Bool
egyenloOttel 5 = True
egyenloOttel _ = False

--tuppleök : ( , )
tupleAnd :: (Bool,Bool) -> Bool
tupleAnd (True,True) = True
tupleAnd _ = False

-- tupel creator

triplecate :: a -> (a,a,a)
triplecate a = (a,a,a)


doubleTheTupple :: (a, b) -> ((a,b),(a,b))
--doubleTheTupple (a,b) = ((a,b),(a,b))
doubleTheTupple a = (a,a)

--Listák
lista = 1 : 2 : 3 : 4 : 5 : []

head' :: [a] -> a
head' (x:xs) = x

tail' :: [a] -> [a]
tail' (x:xs) = xs

--[1..] generál