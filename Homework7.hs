module Homework7 where
import Data.Char
import Data.List

testFile0 :: File 
testFile0 = [" asd  ", "-- Foo bar", "", "\thello world "]

testFileEmptyLines :: File
testFileEmptyLines = repeat []

testFileEmpty :: File 
testFileEmpty = []

type Line = String
type File = [Line]

--magamnak
testLine :: Line
testLine = "hello"
--

countWordsInLine :: Line -> Int
countWordsInLine x = length (words (x))

countWords :: File -> Int
countWords [] = 0
countWords (x:xs) = length ( words (x)) + countWords xs

countChars :: File -> Int
countChars [] = 0
countChars (x:xs) = length ( x ) + countChars xs

capitalizeWord :: String -> String
capitalizeWord (x:xs) = toUpper(x):xs

capitalizeWordsInLine :: Line -> Line
capitalizeWordsInLine (x:xs) = unwords (words (toUpper(x):xs))

isComment :: Line -> Bool
isComment x = ( (take 2 x) == "--")

dropComments :: File -> File
dropComments (x:xs)
    | (isComment x) = dropComments xs
    | otherwise = x : dropComments xs
dropComments _ = []

intToString :: Int -> String
intToString x 
    | x == 0 = ['0']
    | x == 1 = ['1']
    | x == 2 = ['2']
    | x == 3 = ['3']
    | x == 4 = ['4']
    | x == 5 = ['5']
    | x == 6 = ['6']
    | x == 7 = ['7']
    | x == 8 = ['8']
    | x == 9 = ['9']
    | x > 9 = (intToString (x `div` 10)  ++ intToString (mod x 10 ) )

numberLines :: File -> File
numberLines x = numberLinesSeged (zip [1..length(x)]x) where
    numberLinesSeged :: [(Int,Line)] -> File
    numberLinesSeged ((x,xs):ys) = (intToString(x) ++ ": " ++ xs) : numberLinesSeged ys
    numberLinesSeged _ = []

dropTrailingWhitespaces :: Line -> Line
dropTrailingWhitespaces x = reverse(trimBack (reverse x)) where
    trimBack :: Line -> Line
    trimBack (x:xs)
        | (isSpace(x)) = trimBack xs
        | otherwise = x:xs
    trimBack _ = []

replaceTab :: Int -> Char -> [Char]
replaceTab x y
    | (y == '\t') = replicate x ' '
    | otherwise = [y]

replaceTabs :: Int -> File -> File
replaceTabs n (x:xs) = ((replaceTabs n) (concat x)) : ((replaceTabs n) xs)
