module Homework2 where

isSmallPrime :: Int -> Bool
isSmallPrime 2 = True
isSmallPrime 3 = True
isSmallPrime 5 = True
isSmallPrime 7 = True
isSmallPrime _ = False

equivalent :: Bool -> Bool -> Bool
equivalent a b = (a == b)

implies :: Bool -> Bool -> Bool
implies True False = False
implies _ _ = True

invertO :: (Int, Int) -> (Int, Int)
invertO (a , b) = (-a , -b)

isOnNegId :: (Int, Int) -> Bool
isOnNegId (a , b) = b == -a


distance :: (Int, Int) -> (Int, Int) -> Double
distance (a , b) (c , d) = sqrt ( fromIntegral ((c - a)^2 + (d - b)^2))

add :: (Int, Int) -> (Int, Int) -> (Int, Int)
add (a , b) (c , d) = (a*d + c*b , b*d)


multiply :: (Int, Int) -> (Int, Int) -> (Int, Int)
multiply (a , b) (c , d) = (a*c , b*d)

divide :: (Int, Int) -> (Int, Int) -> (Int, Int)
divide (a , b) (c , d) = (a*d , b*c)