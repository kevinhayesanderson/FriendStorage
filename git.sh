for filename in *
do
    echo "${filename} has $(git log --since="2019-12-11" --until="2019-12-27" --pretty=format:%ae ${filename} \ | sort -u | wc -l) commits"
done