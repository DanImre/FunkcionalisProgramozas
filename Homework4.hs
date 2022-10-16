module Homework4 where

--x0 = 
x2 = [[(1,[2,3])]]
x1 = [[(1,[1])],[(1,[1])],[(1,[1]),(1,[1])]]
x0 = [[(1,[2]),(1,[2]),(1,[2])]]

f :: [[(a,[b])]] -> Int
f [(x,xs):[y,ys]]            = 0
f ([_]:[(x,[xs])]:[y,ys]:[]) = 1
f ([(x,y:_:[])]:[])          = 2

howMany :: Char -> String -> Int
howMany c s = length [n | n <- s , n == c]

insertAt :: Int -> a -> [a] -> [a]
insertAt i c s = (take i s) ++ [c] ++ (drop i s)

letterize :: String -> [String]
letterize s = [[n] | n <- s]

swapElems :: [[a]] -> [[a]]
swapElems [] = [] 
swapElems [(x:y:xs)] = [(y:x:xs)] 
swapElems (x:xs) = ((swapElem x):(swapElems xs))
--swapElems (x:xs) = ((xii1 : xii0)) : swapElems xs

swapElem :: [a] -> [a]
swapElem [] = []
swapElem [x] = [x]
--swapElem [x,y] = [y,x]
swapElem (x:y:ys) = (y:x:ys)

isLonger :: [a] -> [b] -> Bool
isLonger [] [] = False
isLonger a [] = True
isLonger [] b = False
isLonger (a:as) (b:bs) = isLonger as bs