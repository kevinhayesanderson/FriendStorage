#!/bin/bash
#FILES=/mnt/c/Users/kevinhayes/Documents/GitHub/FriendStorage
#for f in *
#do
#  filename=$f
#  echo "${filename} has $(git log --since="2019-12-11" --until="2019-12-27" --pretty=format:%ae ${filename} \ | sort -u | wc -l) commits"
#done


for f in $(find . -type f -name "*.cs"); 
do 
    filename=$f
    echo "${filename} has $(git log --since="2019-12-11" --until="2019-12-27" --pretty=format:%ae -- ${filename} \ | sort -u | wc -l) commits"
    #echo "${filename} has $(git log --oneline -- ${filename} | wc -l) commits"
 done >> gitlog.log