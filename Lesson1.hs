module Lesson1 where

one :: Int
two :: Int

one = 1
two = 1 + one

inc :: Int -> Int
inc a = a + 1

inc' :: Int -> Int
inc' a = (+) a 1