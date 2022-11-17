module Lesson7 where
import Data.List
import Data.Char

--dollarjel jobbra 0
--($) f b = f b

--Prelude.all
--all :: (a -> Bool) -> [a] -> Bool
--all f a = length [ n | n <- map f a, n == False] == 0
--all f a = length ( filter (\kk -> not (f kk)) a) == 0

--Pont jobbra 9
--(.) :: (b -> c) -> (a -> b) -> (a -> c)
--(.) f g x = f(g(x))

--Prelude.any
--any :: (a -> Bool) -> [a] -> Bool
--any f a = length ( filter (\kk -> f (kk)) a) > 0
--any p = not . null . filter p

--Prelude.elem
--elem :: Eq => a -> [a] -> Bool
--elem e a = any (\kk -> kk == e) a 
--elem e = any (== e) 

filters :: Eq a => [a] -> [a] -> [a]
filters a b = concatMap (helper a) b where
    helper :: Eq a => [a] -> a -> [a]
    helper a e
        | elem e a = []
        | otherwise = [e]

filters a b = [ n | n <- b , not (elem n a)]

filters a = filter (flip notElem a)

--zipWith :: (a -> b -> c) -> [a] -> [b] -> [c]
--zipWith _ [] _ = []
--zipWith _ _ [] = []
--zipWith f (a:as) (b:bs) = a `f` b : (zipWith f as bs)

--Data.List.group
--group :: Eq a => [a]{-véges-} -> [[a]]
--group [] = []
--group (a:as) = (a:ls) : group rs where
--    (ls, rs) = span (==a) as

compress :: Eq a => [a] -> [(Int,a)]
compress [] = [(0,[])]
compress a = map (\(kk:kks) -> (length(kk:kks),kk)) (group a)

qsort :: Ord a => [a] -> [a]
qsort [] = []
qsort (a:as) = qsort smaller ++ (a : qsort greater) where
    (smaller, greater) = partition (<= a) as