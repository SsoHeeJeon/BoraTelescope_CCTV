using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSunset_Tour : MonoBehaviour
{
    public Sprite[] route1;
    public Sprite[] route2;
    public Sprite[] route3;

    public static Sprite[] Route1;
    public static Sprite[] Route2;
    public static Sprite[] Route3;

    public static List<string> Posadd_K = new List<string>();
    public static List<string> Posadd_E = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        Route1 = new Sprite[route1.Length];
        Route2 = new Sprite[route2.Length];
        Route3 = new Sprite[route3.Length];

        Route1 = (Sprite[])route1.Clone();
        Route2 = (Sprite[])route2.Clone();
        Route3 = (Sprite[])route3.Clone();

        MapInfo();
        PosAddress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapInfo()
    {
        ConnectAPI.mp.Add("street", new MapPos("구포만세거리", "35.2044220", "128.9963560"));
        ConnectAPI.mp.Add("Platform", new MapPos("문화예술플랫폼", "35.206761", "128.997995"));
        ConnectAPI.mp.Add("Gupomarket", new MapPos("구포시장", "35.208715", "129.003776"));
        ConnectAPI.mp.Add("CreateCenter", new MapPos("창조문화활력센터", "35.2065", "129.0045"));
        ConnectAPI.mp.Add("GupoCathedral", new MapPos("구포성당", "35.2059", "129.0017"));
        ConnectAPI.mp.Add("EduPark", new MapPos("솔북이에듀파크", "35.200569", "129.002221"));
        
        ConnectAPI.mp.Add("ArtCenter", new MapPos("북구문화예술회관", "35.2134", "129.0055"));
        ConnectAPI.mp.Add("Castle", new MapPos("구포왜성", "35.2165", "129.0073"));
        ConnectAPI.mp.Add("Young", new MapPos("덕천동 젊음의 거리", "35.2099", "129.0071"));
        ConnectAPI.mp.Add("ManduckT", new MapPos("만덕사지 당간지주", "35.214873", "129.043269"));
        ConnectAPI.mp.Add("SuckbulT", new MapPos("병풍암 석불사", "35.221420", "129.048762"));
        ConnectAPI.mp.Add("RegoTown", new MapPos("만덕레고마을", "35.2077", "129.0398"));
        
        ConnectAPI.mp.Add("HwamyeongPark", new MapPos("화명생태공원", "35.2266", "129.0036"));
        ConnectAPI.mp.Add("Weather", new MapPos("부산기후변화체험교육관", "35.233", "129.0082"));
        ConnectAPI.mp.Add("Sea", new MapPos("부산어촌민속관", "35.2335", "129.0088"));
        ConnectAPI.mp.Add("Daechunchun", new MapPos("대천천애기소", "35.2436", "129.0261"));
        ConnectAPI.mp.Add("ForestPark", new MapPos("화명수목원", "35.251", "129.0429"));
        ConnectAPI.mp.Add("GaramStreet", new MapPos("가람낙조길", "35.2576", "129.0162"));
    }

    public void PosAddress()
    {
        Posadd_K.Add("부산시 북구 구포만세길 109");
        Posadd_K.Add("부산광역시 북구 구포만세길 113");
        Posadd_K.Add("부산광역시 북구 구포시장2길 7");
        Posadd_K.Add("부산광역시 북구 구포제1동 624-12");
        Posadd_K.Add("부산광역시 북구 가람로52번길 44");
        Posadd_K.Add("부산광역시 북구 낙동북로 755");

        Posadd_K.Add("부산광역시 북구 금곡대로46번길 50");
        Posadd_K.Add("부산 북구 덕천1동 산93번지");
        Posadd_K.Add("부산광역시 북구 덕천동 덕천1길 2");
        Posadd_K.Add("부산광역시 북구 만덕2동 784번지");
        Posadd_K.Add("부산광역시 북구 만덕고개길 143-79");
        Posadd_K.Add("부산광역시 북구 만덕제2동 은행나무로23번길 40");

        Posadd_K.Add("부산광역시 북구 금곡동 1511-3");
        Posadd_K.Add("부산광역시 북구 학사로 118");
        Posadd_K.Add("부산광역시 북구 학사로 128");
        Posadd_K.Add("부산 북구 화명동 172 일대");
        Posadd_K.Add("부산광역시 북구 산성로 299");
        Posadd_K.Add("부산광역시 북구 화명동 187-1");

        Posadd_E.Add("109, Gupo Manse-gil, Buk-gu, Busan");
        Posadd_E.Add("113, Gupo Manse-gil, Buk-gu, Busan");
        Posadd_E.Add("7, Gupo Market 2-gil, Buk-gu, Busan");
        Posadd_E.Add("624-12 Gupoje 1-dong, Buk-gu, Busan");
        Posadd_E.Add("44, Garam-ro 52beon-gil, Buk-gu, Busan");
        Posadd_E.Add("755, Nakdong Buk-ro, Buk-gu, Busan");

        Posadd_E.Add("50, Geumgok-daero 46beon-gil, Buk-gu, Busan");
        Posadd_E.Add("93, Deokcheon 1-dong, Buk-gu, Busan");
        Posadd_E.Add("2, Deokcheon 1-gil, Deokcheon-dong, Buk-gu, Busan");
        Posadd_E.Add("784 Mandeok 2-dong, Buk-gu, Busan");
        Posadd_E.Add("143-79 Mandeokgogae-gil, Buk-gu, Busan");
        Posadd_E.Add("40, Ginkgo Namu-ro 23beon-gil, Mandeok 2-dong, Buk-gu, Busan");

        Posadd_E.Add("1511-3 Geumgok-dong, Buk-gu, Busan");
        Posadd_E.Add("118 Haksa-ro, Buk-gu, Busan");
        Posadd_E.Add("128, Haksa-ro, Buk-gu, Busan");
        Posadd_E.Add("172 Hwamyeong-dong, Buk-gu, Busan");
        Posadd_E.Add("299, Sanseong-ro, Buk-gu, Busan");
        Posadd_E.Add("187-1 Hwamyeong-dong, Buk-gu, Busan");
    }
}
