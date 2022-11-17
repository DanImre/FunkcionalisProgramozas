module Zarthelyi2 where
import Data.List
import Data.Char

--1. Kérdés: Egy olyan függvény ami saját magát (vagy saját magát is) meghívja az eredmény keresése közben. 

--2. Kérdés: A fordítási idejű hibák azért keletkeznek, mert a kód nem értelmezhető/fordítható (a kód futatható fájlba alakítója számára). A futási idejű hibák pedig egy 'leforduló' kódban futás közben előforduló hiba, például olyan dolgok amik működhetnek, de rossz paraméter megadásával ütközhetünk hibába, ilyen lehet a 0 val való osztás. 

toTupleList :: [a] -> [(a,a)]
toTupleList [] = []
toTupleList [x] = [(x,x)]
toTupleList (x:xs) = (x,x) : toTupleList xs

toInfinityAndBeyond :: [a] -> [a]
toInfinityAndBeyond a = a ++ reverse a ++ toInfinityAndBeyond a

exactlyTwoTimes :: Eq a => a -> [a] -> Bool
exactlyTwoTimes a b = (length [n | n <- b, n == a]) == 2