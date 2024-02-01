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
        ConnectAPI.mp.Add("street", new MapPos("���������Ÿ�", "35.2044220", "128.9963560"));
        ConnectAPI.mp.Add("Platform", new MapPos("��ȭ�����÷���", "35.206761", "128.997995"));
        ConnectAPI.mp.Add("Gupomarket", new MapPos("��������", "35.208715", "129.003776"));
        ConnectAPI.mp.Add("CreateCenter", new MapPos("â����ȭȰ�¼���", "35.2065", "129.0045"));
        ConnectAPI.mp.Add("GupoCathedral", new MapPos("��������", "35.2059", "129.0017"));
        ConnectAPI.mp.Add("EduPark", new MapPos("�ֺ��̿�����ũ", "35.200569", "129.002221"));
        
        ConnectAPI.mp.Add("ArtCenter", new MapPos("�ϱ���ȭ����ȸ��", "35.2134", "129.0055"));
        ConnectAPI.mp.Add("Castle", new MapPos("�����ּ�", "35.2165", "129.0073"));
        ConnectAPI.mp.Add("Young", new MapPos("��õ�� ������ �Ÿ�", "35.2099", "129.0071"));
        ConnectAPI.mp.Add("ManduckT", new MapPos("�������� �簣����", "35.214873", "129.043269"));
        ConnectAPI.mp.Add("SuckbulT", new MapPos("��ǳ�� ���һ�", "35.221420", "129.048762"));
        ConnectAPI.mp.Add("RegoTown", new MapPos("����������", "35.2077", "129.0398"));
        
        ConnectAPI.mp.Add("HwamyeongPark", new MapPos("ȭ����°���", "35.2266", "129.0036"));
        ConnectAPI.mp.Add("Weather", new MapPos("�λ���ĺ�ȭü�豳����", "35.233", "129.0082"));
        ConnectAPI.mp.Add("Sea", new MapPos("�λ���̹μӰ�", "35.2335", "129.0088"));
        ConnectAPI.mp.Add("Daechunchun", new MapPos("��õõ�ֱ��", "35.2436", "129.0261"));
        ConnectAPI.mp.Add("ForestPark", new MapPos("ȭ������", "35.251", "129.0429"));
        ConnectAPI.mp.Add("GaramStreet", new MapPos("����������", "35.2576", "129.0162"));
    }

    public void PosAddress()
    {
        Posadd_K.Add("�λ�� �ϱ� ���������� 109");
        Posadd_K.Add("�λ걤���� �ϱ� ���������� 113");
        Posadd_K.Add("�λ걤���� �ϱ� ��������2�� 7");
        Posadd_K.Add("�λ걤���� �ϱ� ������1�� 624-12");
        Posadd_K.Add("�λ걤���� �ϱ� ������52���� 44");
        Posadd_K.Add("�λ걤���� �ϱ� �����Ϸ� 755");

        Posadd_K.Add("�λ걤���� �ϱ� �ݰ���46���� 50");
        Posadd_K.Add("�λ� �ϱ� ��õ1�� ��93����");
        Posadd_K.Add("�λ걤���� �ϱ� ��õ�� ��õ1�� 2");
        Posadd_K.Add("�λ걤���� �ϱ� ����2�� 784����");
        Posadd_K.Add("�λ걤���� �ϱ� �������� 143-79");
        Posadd_K.Add("�λ걤���� �ϱ� ������2�� ���೪����23���� 40");

        Posadd_K.Add("�λ걤���� �ϱ� �ݰ 1511-3");
        Posadd_K.Add("�λ걤���� �ϱ� �л�� 118");
        Posadd_K.Add("�λ걤���� �ϱ� �л�� 128");
        Posadd_K.Add("�λ� �ϱ� ȭ�� 172 �ϴ�");
        Posadd_K.Add("�λ걤���� �ϱ� �꼺�� 299");
        Posadd_K.Add("�λ걤���� �ϱ� ȭ�� 187-1");

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
