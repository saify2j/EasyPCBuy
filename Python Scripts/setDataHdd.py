from bs4 import BeautifulSoup
import requests
import csv
import os
from time import sleep
import pandas as pd

ryans = {
         'brand': [],
         'name': [],
         'model': [],
         'link': [],
         'ryans_price': [],
         'pic_link': [],
         'storage':[],
         'description': []
         }
d = {
    'brand': [],
    'name': [],
    'ryans_price': [],
    'star_price': [],
    'star_link': [],
    'ryans_link': [],
    'pic_link': [],
    'storage':[],
    'description': []
}
df = pd.DataFrame(d)
ryan_df = pd.DataFrame(ryans)
with open('Ryans-Final\\ryans-internal-hdd2.csv', 'r', newline='', encoding="utf-8", errors='ignore') as rf:
    reader = csv.reader(rf, delimiter=',')
    i = 0
    for row in reader:
        ryan_df.loc[i] = [row[0]] + [row[1]] + [row[2]] + [row[3][:-2]] + [row[4]] + [row[5]] + [row[6]]+[row[7]]
        i = i + 1

final_names = []
with open('Startech-Final\\startech-hard-disk-drive.csv', 'r', newline='', encoding="utf-8", errors='ignore') as rf:
    reader = csv.reader(rf, delimiter=',')
    j = int(0)
    for row in reader:
        for i in range(0, len(ryan_df)):
            if not final_names.__contains__(row[0]):
                if ryan_df.at[i, 'name'][0:10] == row[0][0:10]:
                    ryan_price = str(ryan_df.at[i, 'ryans_price'])
                    ryan_price = ryan_price.replace(',', '')
                    brand = str(ryan_df.at[i, 'brand'])
                    ryans_link = str(ryan_df.at[i, 'link'])
                    pic_link = str(ryan_df.at[i, 'pic_link'])
                    capacity= str(ryan_df.at[i, 'storage'])
                    description = str(ryan_df.at[i, 'description'])

                    df.loc[j] = [brand] + [row[0]] + [ryan_price] + [str(row[2]).replace(',', '')] + [row[1]] + [
                        ryans_link] + [pic_link] + [capacity] + [description]
                    final_names.append(row[0])
                    j = j + 1
    print()
export_csv = df.to_csv(r'hdd_final-3.csv', index=None, header=True)
