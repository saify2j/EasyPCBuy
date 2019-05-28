from bs4 import BeautifulSoup
import requests
import csv
import os
from time import sleep
import pandas as pd


def get_last_page(url, component):
    main_page = requests.get(url + component+".html")
    soup = BeautifulSoup(main_page.content, 'html.parser')
    # print(soup.prettify())
    number_of_pages = []
    for a in soup.select('.pages li a'):
        number_of_pages.append(a.get_text())

    last_page = int(number_of_pages[-2])
    return last_page


def load_data(url, component, last_page):
    wr = open('Ryans-Final\\ryans-' + component + '.csv', 'a', newline='', encoding="utf-8")
    writer = csv.writer(wr)
    header = ['product_name', 'model', 'link', 'ryans_price', 'brand','picLink','description']
    writer.writerow(header)
    for i in range(1, last_page + 1):
        number = i
        page = requests.get(url + component + ".html?p=" + str(number))
        soup = BeautifulSoup(page.content, 'html.parser')
        names = []
        links = []
        prices = []
        models = []
        descriptions = []
        brands = []
        pic_links =[]

        for a in soup.select('.container h2 a'):
            links.append(a['href'])
            model_name, brand_name, description = load_product_model(a['href'])
            print(model_name)
            brands.append(brand_name)
            names.append(a.get_text(strip=True))
            models.append(model_name)
            descriptions.append(description)
        price_list = soup.find_all('p', attrs={"class": "special-price"})
        for a in soup.find_all('a', attrs={"class": "product-image"}):
            if a.img:
                print(a.img['src'])
                pic_links.append(a.img['src'])

        for j in price_list:
            prices.append(j.get_text()[18:])

        # print(names[0]+" : "+links[0]+" : "+prices[0])

        rows = zip(names, models, links, prices,brands,pic_links,descriptions)


        for row in rows:
                writer.writerow(row)
    print("Writing to file done...")


def load_product_model(link):
    model = "No Model Found"
    brand = "No Brand Specified"
    description = "No Description Found"

    try:
        page = requests.get(link)
        soup = BeautifulSoup(page.content, 'html.parser')
        model = soup.find('table', class_='data-table').find('th', text='Model').find_next_sibling('td').text
        brand = soup.find('table', class_='data-table').find('th', text='Brand').find_next_sibling('td').text
        list_a = soup.find_all('div', attrs={"class": "std"})
        temp = str(list_a[1].get_text())
        temp = temp.strip()
        temp = temp.replace(",", "\n")
        description = temp
        # print(temp)

    except:
        pass
    return model, brand, description


def main():
    url = "https://ryanscomputers.com/components/"
    # components = ["processor",
    #              "desktop-ram",
    #              "mainboard",
    #              "graphics-card"]
    components = ["mainboard"]
    for component in components:
        last_page = get_last_page(url, component)
        load_data(url, component, last_page)



    # d = {'name': [],
    #      'ryans-price': [],
    #      'star-price': []}
    # ryans={'name': [],
    #      'ryans-price': []}
    #
    # df = pd.DataFrame(d)
    # ryan_df= pd.DataFrame(ryans)
    # with open('Ryans-new\\ryans-processor.csv', 'r', newline='') as rf:
    #     reader = csv.reader(rf, delimiter=',')
    #     i = 0
    #     for row in reader:
    #         ryan_df.loc[i] = [row[0]]+[row[3][:-2]]
    #         i = i+1
    # #print(ryan_df)
    # final_names=[]
    # with open('Startech-New-May\\startech-processor.csv', 'r', newline='') as rf:
    #     reader = csv.reader(rf, delimiter=',')
    #     j = int(0)
    #     for row in reader:
    #         for i in range(0,len(ryan_df)):
    #             if not final_names.__contains__(row[0]):
    #                 if ryan_df.at[i, 'name'][0:11] == row[0][0:11]:
    #                     ryan_price= str(ryan_df.at[i, 'ryans-price'])
    #                     ryan_price=ryan_price.replace(',','')
    #
    #
    #                     df.loc[j] = [row[0]] + [ryan_price] + [str(row[3]).replace(',','')]
    #                     final_names.append(row[0])
    #                     j = j + 1
    #     print()
    # export_csv = df.to_csv(r'data_processor.csv', index=None, header=True)


if __name__ == '__main__':
    main()
