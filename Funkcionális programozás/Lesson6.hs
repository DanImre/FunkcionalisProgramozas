module Lesson6 where
import Data.List
import Data.Char

--Saját tipusszinonímák
type Alma = Int
inc' :: Alma -> Alma
inc' a = a + 1

--konstans értékek:
almafa :: Int
almafa = 5

--MyInt nagybetűvel kell kezdődjön, az = jel uténa au 'Int' helyére cak 1 tipus mehet 
newtype MyInt = MyInt Int

--Tobb konstruktora van
data RGB = Red | Green | Blue
    deriving (Show, Eq, Ord) --Így is lehet

{-
instance Ord RGB where
    Blue <= Red = True
    Blue <= Green = True
    Green <= Red = True
    a <= bb = a == b
-}

--Egyenlőség definiálása
{-
instance Eq RGB where
    Red == Red = True
    Blue = Blue = True
    Green = Green = True
    _ == _ = False

-}

--hogy ki tudja írni:
{-
instance Show RGB where
    show Red = "piros"
    show Green = "zold"
    show Blue = "kek"
-}
redSimple :: RGB
redSimple = Red