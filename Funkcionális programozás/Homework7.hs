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

countWordsInLine :: Line -> Int
countWordsInLine x = length (words (x))

countWords :: File -> Int
countWords x = sum (map countWordsInLine x)

countChars :: File -> Int
countChars x = sum (map length x)

capitalizeWord :: String -> String
capitalizeWord (x:xs) = toUpper(x):xs
capitalizeWord _ = []

capitalizeWordsInLine :: Line -> Line
capitalizeWordsInLine x = unwords ( map capitalizeWord (words x))

isComment :: Line -> Bool
isComment x = ( (take 2 x) == "--")

dropComments :: File -> File
dropComments x = [n | n <- x , not (isComment n)]

numberLines :: File -> File
numberLines x = [ show n ++ ": " ++ s | (n,s) <- (zip [1..length(x)] x)]

dropTrailingWhitespaces :: Line -> Line
dropTrailingWhitespaces [] = []
dropTrailingWhitespaces x = reverse (dropWhile isSpace (reverse x))

replaceTab :: Int -> Char -> [Char]
replaceTab x y
    | (y == '\t') = replicate x ' '
    | otherwise = [y]

replaceTabs :: Int -> File -> File
replaceTabs n x = map (concatMap (replaceTab n)) x
