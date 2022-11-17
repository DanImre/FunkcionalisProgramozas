module Homework10 where
import Data.Char
import Data.List

isJust :: Maybe a -> Bool
isJust (Just _) = True
isJust _ = False

fromJust :: Maybe a -> a
fromJust (Just a) = a
fromJust (Nothing) = error "Rossz"

catMaybes :: [Maybe a] -> [a]
catMaybes l = [ n | (Just n) <- l ]

mapMaybe :: (a -> Maybe b) -> [a] -> [b]
mapMaybe f l = [ n | (Just n) <- (map f l)]

type Username = String
type Password = String

data Privilege = Simple | Admin
    deriving (Eq, Show)

data Cookie = LoggedOut | LoggedIn Username Privilege
    deriving (Eq, Show)

data Entry = Entry Password Privilege [Username]
    deriving (Eq, Show)

type Database = [(Username, Entry)]

richard, charlie, carol, david, kate :: (Username, Entry)
richard = ("Richard", Entry "password1" Admin  ["Kate"])
charlie = ("Charlie", Entry "password2" Simple ["Carol"])
carol   = ("Carol",   Entry "password3" Simple ["David", "Charlie"])
david   = ("David",   Entry "password4" Simple ["Carol"])
kate    = ("Kate",    Entry "password5" Simple ["Richard"])

testDB :: Database
testDB = [ richard, charlie, carol, david, kate ]

testDBWithoutCarol :: Database
testDBWithoutCarol =
  [ ("Richard", Entry "password1" Admin  ["Kate"])
  , ("Charlie", Entry "password2" Simple [])
  , ("David",   Entry "password4" Simple [])
  , ("Kate",    Entry "password5" Simple ["Richard"])
  ]

password :: Entry -> Password
password (Entry x _ _) = x

privilege :: Entry -> Privilege
privilege (Entry _ x _) = x

friends :: Entry -> [Username]
friends (Entry _ _ x) = x

mkCookie :: Username -> Password -> Entry -> Cookie
mkCookie username password (Entry entryPassword entryPrivilage _)
    | (password == entryPassword) = LoggedIn username entryPrivilage
    | otherwise = LoggedOut

--((databaseUserName, (Entry entryPassword entryPrivilage _)) : xs)
login :: Username -> Password -> Database -> Cookie
login username password l
    | isJust (lookup username l) = mkCookie username password (fromJust( lookup username l))
    | otherwise = LoggedOut

updateEntry :: Username -> (Username, Entry) -> Maybe (Username, Entry)
updateEntry u (entryU, (Entry entryP entryPrivi entryFriends))
    | (u == entryU) = Nothing
    | otherwise = Just (entryU, Entry entryP entryPrivi [ n | n <- entryFriends, n /= u])

{-
deleteUser :: Cookie -> Username -> Database -> Database
deleteUser (LoggedIn _ Admin) u l = [ n | n <- l, isJust(updateEntry u n)]
deleteUser _ _ l = l
-}

deleteUser :: Cookie -> Username -> Database -> Database
deleteUser (LoggedIn _ Admin) u l = [ fromJust n | n <- (map (updateEntry u) l), isJust(n)]
deleteUser _ _ l = l