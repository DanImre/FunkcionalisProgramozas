module Lesson2 where

inc :: Int -> Int
inc a = a + 1;

exp, exp1, exp2 :: Int
exp = (inc. inc . inc . inc) 1
exp1 = inc(inc(inc(inc(1))))
exp2 = inc $ inc $ inc $ inc 1

--for haskellül:

returnFirst :: a -> b