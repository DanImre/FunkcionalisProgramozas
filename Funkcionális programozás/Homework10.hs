module LastHomework where
import Data.Char
import Data.List

align :: Int -> String -> String
align n x = addSpaces (n - length x) x where
    addSpaces :: Int -> String -> String
    addSpaces n x
        | (n > 0) = " " ++ addSpaces (n-1) x
        | otherwise = x

modify :: (a -> Maybe a) -> [a] -> [a]
modify f (x:xs) = seged (f x) xs where
    seged :: Maybe a -> [a] -> [a]
    seged (Just x) xs = x:xs
    seged _ xs = xs
modify _ [] = []

data RPS = Rock | Paper | Scissors
    deriving (Show,Eq)

beats :: RPS -> RPS
beats Paper = Rock
beats Scissors = Paper
beats Rock = Scissors

firstBeats :: [RPS] -> [RPS] -> Int
firstBeats (x:xs) (y:ys)
    | (beats x == y) = 1 + firstBeats xs ys
    | otherwise = firstBeats xs ys
firstBeats _ _ = 0