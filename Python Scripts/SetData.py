from bs4 import BeautifulSoup
import requests
import csv
import os
from time import sleep
import pandas as pd


ryans={'name': [],
       'model': [],
       'link': [],
       'ryans_price': [],
       'brand': [],
       'pic_link':[],

       'description': []
       }
d = {
     'brand':[],
     'name': [],
     'ryans_price': [],
     'star_price': [],
     'star_link':[],
     'ryans_link':[],
     'pic_link':[],
     'description':[]
     }
df = pd.DataFrame(d)
ryan_df = pd.DataFrame(ryans)
with open('Ryans-Final\\ryans-mainboard.csv', 'r', newline='',encoding="utf-8",errors='ignore') as rf:
    reader = csv.reader(rf, delimiter=',')
    i = 0
    for row in reader:
        ryan_df.loc[i] = [row[0]]+[row[1]]+[row[2]]+[row[3][:-2]]+[row[4]]+[row[5]]+[row[6]]
        i = i+1
# print(ryan_df.at[4,'ryans_price'])


final_names = []
with open('Startech-Final\\startech-motherboard.csv', 'r', newline='',encoding="utf-8",errors='ignore') as rf:
    reader = csv.reader(rf, delimiter=',')
    j = int(0)
    for row in reader:
        for i in range(0, len(ryan_df)):
            if not final_names.__contains__(row[0]):
                if ryan_df.at[i, 'name'][0:20] == row[0][0:20]:
                    ryan_price = str(ryan_df.at[i, 'ryans_price'])
                    ryan_price = ryan_price.replace(',','')
                    brand = str(ryan_df.at[i, 'brand'])
                    ryans_link=  str(ryan_df.at[i, 'link'])
                    pic_link =  str(ryan_df.at[i, 'pic_link'])
                    description =  str(ryan_df.at[i, 'description'])

                    df.loc[j] = [brand] + [row[0]] + [ryan_price] + [str(row[2]).replace(',','')]+[row[1]]+[ryans_link]+[pic_link]+[description]
                    final_names.append(row[0])
                    j = j + 1
    print()
export_csv = df.to_csv(r'hdd_final.csv', index=None, header=True)
