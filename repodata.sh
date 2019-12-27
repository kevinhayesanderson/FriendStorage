#!/bin/sh
FILETYPE = "*.cs"
FILENAME = "App"
FROMDATE = "2019-12-11"
TODATE = "2019-12-27"
echo "Filters"
echo "File type filter: $FILETYPE"
echo "File name filter: ${FILENAME}"
echo "Date range filter: --since=${FROMDATE} --until=${TODATE}"