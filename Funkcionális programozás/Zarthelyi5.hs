module Zarthelyi5 where
import Data.List
import Data.Char

-- Mire jó a Maybe típus? Írj rá példát! Maybe típusnak 2 lehetséges 'értéke' van, Nothing és Just, ez alapján el lehet dönteni egy függvény visszatérési értéke alapján, hogy helyesen futott e le, vagy nem megfelelő paraméterekkel dolgozott. Például ha az osztást akarnánk megcsinálni, ott 2 paraméter van, de ha valamit 0 val akarnánk osztani akkor hiba helyett Nothing-ot küldhetünk vissza, ezzel tudatva, hogy nem tudta megoldani.
-- Mi a különbség a lista és a rendezett n-esek (tuple-k) között? A listához hozzá lehet fűzni elemeket, míg a tuple-ök fix méretűek.

condConvOdd :: Integral a => (a -> Bool) -> a -> a
condConvOdd f x
    | (x `mod` 2 == 0 && f x) = x + 1
    | otherwise = x

maybeAnd :: Maybe Bool -> Maybe Bool -> Maybe Bool
maybeAnd (Just x) (Just y) = Just (x && y)
maybeAnd _ _ = Nothing

data SchoolStudent = SchoolStudent String Double Int

instance Show SchoolStudent where
   show (SchoolStudent nev atlag osztaly) = "SchoolStudent " ++ show nev ++ " " ++ show atlag ++ " " ++ show osztaly

instance Eq SchoolStudent where
   (==) (SchoolStudent nev atlag osztaly) (SchoolStudent nev2 atlag2 osztaly2) = (nev == nev2 && atlag == atlag2 && osztaly == osztaly2)

bestStudent :: [SchoolStudent] -> Int -> Double
bestStudent ((SchoolStudent nev atlag osztaly):xs) o
    | (osztaly /= o) = bestStudent xs o
    | (atlag > bestStudent xs o) = atlag
    | otherwise = bestStudent xs o
bestStudent [] o = 0