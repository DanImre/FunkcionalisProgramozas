module Homework4 where

--x0 = 
x2 = [[(1,[2,3])]]
x1 = [[(1,[2]),(1,[2]),(1,[2]),[]]]
x0 = [[(1,[2]),(1,[2]),(1,[2])]]

f :: [[(a,[b])]] -> Int
f [(x,xs):[y,ys]]            = 0
f ([_]:[(x,[xs])]:[y,ys]:[]) = 1
f ([(x,y:_:[])]:[])          = 2