using StarCapturePlan.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “基本页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace StarCapturePlan
{
    public sealed partial class Details : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private SystemMediaTransportControls systemControls;
        ThePlanets[] theplanets = new ThePlanets[19];
        String[] introduction = new String[19];
        String[] Story = new String[19];
        int number;
        public Details()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            systemControls = SystemMediaTransportControls.GetForCurrentView();
            systemControls.ButtonPressed += SystemControls_ButtonPressed;
            systemControls.IsPlayEnabled = true;
            systemControls.IsPauseEnabled = true;

            Story[0] = "        Mercury是罗马神话中的信使之神，说白了就是负责给主神送情报之类，所以是神话中最能跑腿的。水星对应希腊神话中的信使Helmos。水星距太阳很近，根据万有引力定律，离太阳最近的天体跑的最快，因此以Mercury命名水星实属合情合理。"+ "\r\n        在中国，最早将水星称为辰星，因为它在地球轨道内，我们能看到它的时间，只有大清早或黄昏时距离地平线不过一辰的区域内（一日有12时辰，分别为子丑寅卯辰巳午未申酉戌亥，故一辰为360/12=30度），所以称为辰星。";
            Story[1] = "        金星在中国古代神话中就占据了不容忽视的地位。在我国本土宗教——道教中，太白金星可谓是核心成员之一，论地位仅在三清（太上老君，元始天尊，通天教主）之下。最初道教的太白金星神是位穿着黄色裙子，戴着鸡冠，演奏琵琶的女神，明朝以后形象变化为一位童颜鹤发的老神仙，经常奉玉皇大帝之命监察人间善恶，被称为西方巡使。在我国古典小说中，多次出现太白金星的传奇故事，如脍炙人口的《西游记》中，太白金星就是个多次和孙悟空打交道的好老头。\r\n        此外，在与金星相关的传说中，最具有传奇色彩的应该算是关于唐代大诗人李白的故事了。传说李白的出生不同寻常，乃是他的母亲梦见太白金星落入怀中而生，因此取名李白，字太白。\r\n        由于金星在夜空中耀眼夺目，所以在希腊与罗马神话中，金星是爱与美的化身——维纳斯女神。维纳斯（Venus）是罗马人对她的美称，意思是“绝美的画”，在希腊神话中她叫阿弗罗狄忒（Aphrodite），意思是为“上升的泡沫”，因为传说她是在海面上起的泡沫之中诞生的。维纳斯爱上了战神马尔斯（火星），并为他生下了几个儿女，其中包括小爱神丘比特。\r\n        金星虽然观测耀目，但并非总是代表着吉祥。它时而在东方高悬，时而在西方闪耀，让人捉摸不透，恐惧也就因此而生。对玛雅人和阿兹特克人来说，它既隐喻死亡，又象征复活。它是阿兹特克人的神魁扎尔科亚特尔，能使灭绝的人借着从死人王国中偷来的骨架复活，并用这位神灵赐予的血再生。古代腓尼基人。犹太人都认为它是恶魔的化身，是一颗恶星，古代墨西哥人也害怕金星，在黎明时总要关闭门窗，挡住它的光芒。他们认为，金星的光芒会带来疾病。当然这些传说都是因为古人不了解天体运动规律而臆想出来的唯心主义观念，其实金星就是金星，无关人间祸福。总之，福星也好，祸星也罢，金星永远是夜空中最亮的明星。";
            Story[2] = "        Mars是罗马神话中的战争之神，战争离不了嗜血和屠戮，而火星呈红色，正象征这嗜血和疯狂，因此西方人使用战神Mars的名字来命名腥红色的火星。Mars相当于希腊神话中的战神Ares。" + "\r\n        我国古书上将火星称为“荧惑”，西方古代（古罗马）称为“Mars”在我国古代，火星被称为荧惑，因其光度常有变化，顺行逆行使人迷惑，故名。因为其善变（光度和顺逆行的变化），古人认为它的顺逆行和亮度变化乃是上天给的暗示，荧惑运行时遇到哪个星官哪颗星官所代表的朝廷官员就要倒霉，古代有不少官员就是这么被坑死的。"
                            + "\r\n        火星的有两个卫星，分别为火卫一Phobos和火卫二Deimos，这两颗卫星都是用战神Ares的两个儿子命名的。两个人的名字意思都为“恐怖、可怕”，想想战争给人的感觉你就知道了。";
            Story[3] = "        木星是太阳系最大的行星，而其名称则来自神话中最强大的神主Jupiter。 Jupiter 是罗马神话中的主神名称，这个名字由Jove pater演变而来，意思是【天父】。Jove对应希腊神话中的宙斯Zeus。 "
                            + "\r\n        我们的古人也称木星为岁星，因为人们发现他的运行周期约为12年（实际上的周期为11.86年），故认为其一岁走过一个地支，所以命名其为岁星。后来发现有一些小偏差，古人便将黄道十二等分[①]，用一个假想的等效于岁星的星来纪年，这颗星被称为太岁。人们常说的“太岁头上动土”即与此有关。"
                            + "\r\n        木星现在已发现63颗卫星，其中49颗被正式命名。这些名字一般来自于被宙斯所强行霸占的少女，比如木卫一Io、木卫二Europa、木卫四Callisto、木卫十三Leda；还有一些是宙斯的女儿们，比如木卫四十Mneme 、木卫四十一Aoede、木卫四十二Thelxinoe、木卫四十三Arche、木卫四十四Callichore；当然，还有像木卫四这种被他强占的小伙子Ganymede。";
            Story[4] = "        Saturn是罗马神话中的农神，也是时间之神，他对应希腊神话中的时间之神Cronos。Cronos是第二代神系中提坦神Titans的领袖，他带领提坦神族打败了以其父Uranus领导的第一代神系，后来又被其子Zeus领导的第三代神系所打倒，并被关押在地狱深渊Tartarus之中。土星是太阳系第二大行星，仅次于木星Jupiter，而Saturn则是神话中仅次于Jupiter的大神。";
            Story[5] ="         根据贺西欧德的版本，万物未出现之前，世界是一片空无，希腊人称之为“混沌”。后来混沌生下了盖娅（大地），盖娅又生了许多小孩，其中第一个诞生出来的就是天空——乌拉诺斯（天王星），接着盖娅又生出了高山和大海。因为天空遥不可及，所以天王星代表的是一种心理上的疏远状态。乌拉诺斯后来取盖娅为妻而开始紧紧地覆盖住她，他们成了奥林匹斯山众神的父母及祖父母，因此天王星也象征着祖父母。他们的子女包括独眼巨人伯朗特斯（（雷声）、史帝诺普斯（闪电）及阿尔格斯（霹雳）——他们都只有一只眼睛，长在前额的中央，另外还有十二位泰坦神只，以及三位百臂巨人（各自有五十个头），一百双手臂）。";
            Story[6] = "aaaaaaaaa";
            Story[6] = "aaaaaaaaa";
            Story[7] = "aaaaaaaaa";
            Story[8] = "aaaaaaaaa";
            Story[9] = "aaaaaaaaa";
            Story[10] = "       在希腊神话中，爱与美的女神阿佛罗狄忒和她的爱子厄洛斯有一天在幼发拉底河边散步，突然遭到扰乱诸神宴会的怪物提风的袭击。在一阵慌乱中，两人化身成为鱼逃走，但为了怕彼此走失，阿佛罗狄忒就将身上的丝带系在鱼尾上。后来这个形状被天神看见了，就把它们放置在天上，成为西鱼及北鱼，变成了双鱼座。 ";
            Story[11] = "aaaaaaaaa";
            Story[12] = "aaaaaaaaa";
            Story[13] = "aaaaaaaaa";
            Story[14] = "aaaaaaaaa";
            Story[15] = "aaaaaaaaa";
            Story[16] = "aaaaaaaaa";
            Story[17] = "aaaaaaaaa";
            Story[18] = "aaaaaaaaa";
          


            introduction[0] = "        水星（Mercury），八大行星中离太阳最近的行星，中国古代称为辰星。"
                            + "\r\n        发现时间：公元前3000年左右"
                            + "\r\n        发现者：闪族人"
                            + "\r\n        平均密度：5.427g/cm³"
                            + "\r\n        平均半径：2440KM"
                            + "\r\n        距太阳距离：5800万千米"
                            + "\r\n        表面温度：427℃（白天）、-183℃（夜晚）"
                            + "\r\n        公转周期：约88日"
                            + "\r\n        自转周期：约58.5日";
            introduction[1] = "        金星（Venus），离太阳第二近的行星。"
                            + "\r\n        平均密度：5.24g/cm³"
                            + "\r\n        平均半径：6051.8KM"
                            + "\r\n        距太阳距离：107,476,259千米~ 108,942,109千米"
                            + "\r\n        表面温度：465℃~485℃"
                            + "\r\n        公转周期：224.7天"
                            + "\r\n        自转周期：243天";
            introduction[2] ="         火星（Mars），太阳系内从内到外的第四课行星，也是距离地球最近的行星，也是各方面与地球最为相似的行星。火星地表的氧化铁形成了其橘黄色的外表。"
                            + "\r\n        发现时间：史前年代"
                            + "\r\n        平均密度：3.94g/cm³"
                            + "\r\n        平均半径：3397KM"
                            + "\r\n        距太阳距离：22794万千米"
                            + "\r\n        表面温度：-133℃~27℃，平均-63℃"
                            + "\r\n        公转周期：687天"
                            + "\r\n        自转周期：24.6229h";
            introduction[3] = "        木星(Jupiter)，太阳系内由内向外的第五颗行星。太阳系内行星中体积和质量均最大的行星。（质量为其他七大行星质量总和的2.5倍）。木星是一个气态巨行星。气态行星没有实体表面，它们的气态物质密度随深度的变大而不断加大。我们所看到的通常是大气中云层的顶端，压强比1个大气压略高。 木星主要由氢和氦组成，它是由86%的氢和14%的氦组成的，中心温度估计高达30500℃。"
                            + "\r\n        发现时间：1610年"
                            + "\r\n        发现者：伽利略"
                            + "\r\n        平均密度：1.326g/cm³"
                            + "\r\n        平均半径：71493.5KM"
                            + "\r\n        表面温度：其表面有效温度值为-168℃，而地球观测值为-139℃。"
                            + "\r\n        公转周期：4332.71 日"
                            + "\r\n        自转周期：9小时50分30秒";
            introduction[4] = "        土星(Saturn)至太阳距离（由近到远）位于第六，体积则仅次于木星。 土星有一个显著的行星环（可以通过望远镜直接观测），主要的成分是冰的微粒和较少数的岩石残骸以及尘土。"
                            + "\r\n        在我国古代，土星被称为填星，古人观察到它约二十八年运行一个周天（实际周期为29.5年），相当于其坐镇天上的二十八宿，故曰“岁填一宿”。"
                            + "\r\n        已经确认的土星的卫星总共有62颗。其中，土卫六是土星系统中最大和太阳系中第二大的卫星，半径2575km。"
                            + "\r\n        发现时间：1684年"
                            + "\r\n        发现者：伽利略、卡西尼、惠更斯"
                            + "\r\n        平均密度：0.70g/cm³"
                            + "\r\n        平均半径：60268KM"
                            + "\r\n        表面温度：-191.15℃～-130.15℃"
                            + "\r\n        公转周期：29.53216年"
                            + "\r\n        自转周期：10.546h";
            introduction[5] = "        天王星（Uranus）是太阳系由内向外的第七颗行星（18.37~20.08天文单位），其体积在太阳系中排名第三（比海王星大），质量排名第四（小于海王星），几乎横躺着围绕太阳公转。"
                            + "\r\n        天王星大气的主要成分是氢和氦，还包含较高比例的由水、氨、甲烷等结成的“冰”，与可以探测到的碳氢化合物。天王星是太阳系内大气层最冷的行星，最低温度只有49K（−224℃）。其外部的大气层具有复杂的云层结构，水在最低的云层内，而甲烷组成最高处的云层。 相比较而言，天王星的内部则是由冰和岩石所构成。"
                            + "\r\n        Uranus是希腊神话中的第一代神主，为天空之神，罗马神话也照搬了这个名字。之所以用Uranus命名天王星，主要因为挨着他的儿子Saturn。Uranus意思为‘天空’，故缪斯九女神中司掌天文的女神名为Urania。"
                            + "\r\n        天王星的有27颗卫星，这些卫星名字都比较个性，他们大都来自莎士比亚戏剧中的人物，比如天卫一 Ariel、天卫二 Umbriel、天卫三 Titania、天卫四 Oberon、天卫五 Miranda、天卫七 Ophelia、天卫十一 Juliet。"
                            + "\r\n        发现时间：1781年3月"
                            + "\r\n        发现者：威廉·赫歇尔"
                            + "\r\n        平均密度：1.318g/cm³"
                            + "\r\n        平均半径：25559KM"
                            + "\r\n        表面温度：-180℃"
                            + "\r\n        公转周期：84.323326年"
                            + "\r\n        自转周期：17时14分24秒";
            introduction[6] = "        海王星（Neptune）是八大行星中的远日行星，按照行星与太阳的距离排列海王星是第八颗行星，直径上第四大行星。由于它那荧荧的淡蓝色光，所以西方人用罗马神话中的海神——“尼普顿”的名字来称呼它。在中文里，把它译为海王星。"
                            + "\r\n        Neptune是罗马神话中的海神，相当于希腊神话中的海神波塞冬Posidon，波塞冬是宙斯的哥哥。在对提坦神的战争中第三代神系取得了胜利后，宙斯和他的两个哥哥通过抓阄决定各自的属地，结果宙斯抓到了天空，波塞冬抓到了海洋，哈戴斯Hades抓到了冥界[③]。海王星的符号为他武器三叉戟Trident【三个齿】。之所以用Neptune命名海王星，因为其呈海蓝色。海王星的13颗卫星，这些卫星大多用海神的情人、子女命名，比如海卫一 Triton 、海卫二 Nereid、海卫五 Despina 、海卫六 Galatea、海卫九 Halimede、海卫十 Psamathe。"
                            + "\r\n        海王星的大气层以氢和氦为主，还有微量的甲烷，大气层中的甲烷，是使行星呈现蓝色的原因之一。海王星有着太阳系最强烈的风暴，测量到的风速高达2100km/h。海王星云顶的温度是－218 ℃（55K），因为距离太阳最远，是太阳系最冷的地区之一。海王星核心的温度约7000℃，可以和太阳的表面比较，也和大多数已知的行星相似。"
                            + "\r\n        海王星在1846年9月23日被发现，是唯一利用数学预测而非有计划的观测发现的行星。天文学家利用天王星轨道的摄动推测出海王星的存在与可能的位置，迄今只有旅行者2号曾经在1989年8月25日拜访过海王星。"
                            + "\r\n        发现时间：1846年9月23日"
                            + "\r\n        发现者：奥本·勒维耶、伽勒,亚当斯"
                            + "\r\n        平均密度：1.66g/cm³"
                            + "\r\n        平均半径：24766KM"
                            + "\r\n        表面温度：-214℃"
                            + "\r\n        公转周期：60192日"
                            + "\r\n        自转周期：15h57min59s";
            introduction[7] = "        双子座（拉丁语：Gemini），面积513.76平方度，占全天面积的1.245%，在全天88个星座中，面积排行第三十位。双子座中亮于5.5等的恒星有47颗，最亮星为北河三（双子座β），视星等为1.14。每年1月5日子夜双子座中心经过上中天。纬度变化位于+90°和60°之间可全见。"
                            + "\r\n        向东北方向延长猎户座β星和α星的连线，可以碰到两颗相距不远的亮星，其中亮一些的是双子座β星，亮度为1.14等。稍微暗点儿的是双子座α星，亮度为1.97等。从α星开始的τ、ε、μ一串星和从β星开始的δ、ζ、γ另一串星几乎平行，它们被想象成友爱的两兄弟——卡斯托和普尔尤克斯。"
                            + "\r\n        双子座的西边是金牛座，东边是比较暗淡的巨蟹座。御夫座和非常不明显的天猫座位于它的北边，麒麟座和小犬座位于它的南边。双子座有两颗非常亮的星—北河三和北河二。其它的星都比较暗，只有γ是在城市灯光下也能被看到的，但在远离灯光污染的地方，可以看到稀薄的银河从双子座西部经过。"
                            + "\r\n        天体名称：双子座"
                            + "\r\n        拉丁名：Gemini"
                            + "\r\n        亮星数目：4"
                            + "\r\n        最亮星：北河三（双子座β），视星等1.1"
                            + "\r\n        最佳观测时间：2月"
                            + "\r\n        最佳观测纬度：+90°和−60°之间，完全可见区域90°N-55°S";
            introduction[8] = "        金牛座(Taurus)，面积797.25平方度，占全天面积的1.933%，在全天88个星座中，面积排行第十七。金牛座中亮于5.5等的恒星有98颗，最亮星为毕宿五（金牛座α），视星等为0.85。每年11月30日子夜金牛座中心经过上中天。"
                            + "\r\n        金牛座中最有名的天体，就是“两星团加一星云”。  连接猎户座γ星和毕宿五，向西北方延长一倍左右的距离，有一个最著名的疏散星团——昴星团（M45，亦称七姊妹星团）。眼力好的人，可以看到这个星团中的七颗亮星，所以我国古代又称它为“七簇星”。昴星团距离我们450光年，它的半径达13光年，用大型望远镜观察， 可以发现昴星团的成员有280多颗星。现在肉眼可以看到6颗亮星。另一个疏散星团叫毕星团，它是一个移动星团，就位于毕宿五附近，但毕宿五并不是它的成员。毕星团距离我们143光年，是离我们最近的星团了。毕星团用肉眼可以看到5、6颗星，实际上它的成员大约有300颗。"
                            + "\r\n        金牛座ζ星的附近，还有一个著名的大星云，英国的一位天文学家根据它的形状把它命名为“蟹状星云”。本世纪的天文学家推断出蟹状星云是1054年一次超新星爆发的产物。而1054年的超新星爆发，在我国古代天文学文献中有十分详细的记载。"
                            + "\r\n        天体名称：金牛座"
                            + "\r\n        拉丁名：Taurus"
                            + "\r\n        亮星数目：3"
                            + "\r\n        最亮星：毕宿五（金牛座α，视星等0.85）"
                            + "\r\n        最佳观测时间：1月"
                            + "\r\n        最佳观测纬度：+90°和−69°之间（北半球均可见），完全可见区域88°N-58°S";
            introduction[9] = "        白羊座(Aries) 是黄道第一星座，位于金牛座西南，双鱼座东面。白羊座是一个很暗的小星座，里面只有紧挨着的亮度为2等的α星和2.6等的β星稍微显著些。星座中主要的三颗星排列的形状像是一把老式手枪，从秋末直到春天来到，它总在天空中闪烁着微光。"
                            + "\r\n        每年12月中旬晚上八九点钟的时候，白羊座正在我们头顶。秋季星空的飞马座和仙女座的四颗星组成了一个大方框，从方框北面的两颗星引出一条直线，向东延长一倍半的距离，就可以看到白羊座了。其中有二颗最明亮的星星就是白羊座的两只角。"
                            + "\r\n        白羊座虽然不起眼，但在天文学上，它的地位还是很重要的。2000年以前的春分点就在白羊座，现在的春分点已经移到双鱼座。每年约4月18日到5月14日太阳在白羊座中运行，黄道上的谷雨和立夏两个节气点就在这个星座。"
                            + "\r\n        天体名称：白羊座"
                            + "\r\n        拉丁名：Aries"
                            + "\r\n        亮星数目：2"
                            + "\r\n        最亮星：娄宿三（白羊座α，视星等2.00）"
                            + "\r\n        最佳观测时间：12月"
                            + "\r\n        最佳观测纬度：+85°和−75°";
            introduction[10] = "        双鱼座（拉丁语：Pisces，天文符号：♓）是黄道星座之一，面积889.42平方度，占全天面积的2.156%，在全天的88个星座中，面积排行第十四。双鱼座每年9月27日子夜中心经过上中天。双鱼座中亮于5.5等的恒星有50颗，最亮星为右更二（双鱼座η），视星等为3.62。现在的春分点位于霹雳五（双鱼座ω）附近。纬度变化位于+90°和−65°之间可全见。"
                            + "\r\n        从星图上看，双鱼座中位于秋季四边形正南的这几颗星可以看成是一条鱼（西鱼），四边形的飞马座β星和仙女座α星向东延长一倍碰到的那几颗暗星是另一条鱼（北鱼）。而位于两条鱼之间的， 以α星为顶点的“V”则是栓住它们的“绳子”。"
                            + "\r\n        希腊神话中双鱼座代表的是阿佛洛狄忒和厄洛斯在水中的化身。阿佛洛狄忒为了逃避大地女神盖亚之子巨神提丰攻击而变成鱼躲在尼罗河（一说幼发拉底河）。之后她发现忘记带上自己的儿子厄洛斯一起逃走，于是又上岸找到厄洛斯。为防止与儿子失散，她将两人脚绑在一起，随后两人化为鱼形，潜进河中。事后宙斯将阿佛洛狄忒首先化身的鱼提升到空中成为南鱼座，而她和厄罗斯化身的绑在一起的两条鱼则称为双鱼座。"
                            + "\r\n        天体名称：双鱼座"
                            + "\r\n        拉丁名：Pisces"
                            + "\r\n        亮星数目：0"
                            + "\r\n        最亮星：右更二（双鱼座η），视星等3.62"
                            + "\r\n        最佳观测时间：11月"
                            + "\r\n        最佳观测纬度：83°N-56°S";
            introduction[11] = "        宝瓶座（拉丁语名称Aquarius，）黄道12星座之一，面积979.85平方度，占全天面积的2.375%，在全天88个星座中，面积排行第十位。宝瓶座中亮于5.5等的恒星有56颗，最亮星为虚宿一（宝瓶座β），视星等为2.90。每年8月25日子夜宝瓶座中心经过上中天。纬度变化位于+65°和−-90°之间可全见。"
                            + "\r\n        宝瓶座是一个大但暗的星座，把飞马座的β和α星向南延伸到一倍半远的地方，有一片比较暗的星，这里的一大片暗星就组成了宝瓶座。宝瓶座虽然贵为黄道星座（在黄道星座中，宝瓶座又被称为“水瓶座”），但里面却没有亮星，最亮的也只是3m。"
                            + "\r\n        天体名称：宝瓶座"
                            + "\r\n        拉丁名：Aquarius"
                            + "\r\n        亮星数目：2"
                            + "\r\n        最亮星：虚宿一（宝瓶座β），视星等2.9"
                            + "\r\n        最佳观测时间：10月"
                            + "\r\n        最佳观测纬度：+65°和−-90°之间";
            introduction[12] = "        摩羯座别名山羊座，纬度变化位于+60°和90°之间可全见。是黄道十二星座之一，面积413.95平方度，占全天面积的1.003%，在全天88个星座中，面积排行第四十位。摩羯座中亮于5.5等的恒星有31颗，最亮星为垒壁阵四（摩羯座δ），视星等为2.81。每年8月8日子夜摩羯座中心经过上中天。"
                            + "\r\n        摩羯座组成一个倒三角形结构，在黑暗的夜晚很容易辨别。对于天文爱好者来说，摩羯座没有多少有趣的星体，这个区域的星系都很微弱不显著。摩羯座有一个梅西耶天体球状星团M30。这个南天星座尽管没有一颗亮星，但轮廓相当清楚。"
                            + "\r\n        早在5000年前，就已经有了摩羯座，即海中之豚的星象描述。摩羯座亮星组成一个倒三角形结构，古人将其称为“神仙之门”，认为从地上各种名利是非解脱出来的人，其灵魂就可以通过此门登上天国。"
                            + "\r\n        在古中国的星座系统，摩羯座属于牛宿天区。北宋文学家的名句“月出于东山之上，徘徊于斗牛之间”(苏轼《赤壁赋》)中的牛就是指牛宿。"
                            + "\r\n        天体名称：摩羯座（又名山羊座）"
                            + "\r\n        拉丁名：Capricornus"
                            + "\r\n        亮星数目：0"
                            + "\r\n        最亮星：垒壁阵四 （摩羯座δ），视星等3.0"
                            + "\r\n        最佳观测时间：9月"
                            + "\r\n        最佳观测纬度：+60°和−90°之间";
            introduction[13] = "        人马座（拉丁语：Sagittarius,翻译后为射手座）是一个南天黄道带星座，面积867.43平方度，占全天面积的2.103%，在全天88个星座中，面积排行第十五。人马座中亮于5.5等的恒星有65颗，最亮星为箕宿三（人马座ε），视星等为1.85。每年7月7日子夜人马座中心经过上中天。"
                            + "\r\n        夏夜，从天鹰座的牛郎星沿着银河向南就可以找到它。因为银心就在人马座方向，所以这部分银河是最宽最亮的。人马座正对着银心方向，所以它里面的星团和星云特别多。在南斗σ和λ两星连线向西延长一倍的地方，可以看到一小团云雾样的东西，这其实是个星云。在望远镜里看上去，它是由三块红色的光斑组成的，十分好看，被称为“三叶星云”。人马座里的星云还有不少，比如在南斗斗柄μ星的北面，有个星云很像马蹄子的形状，因此被称为“马蹄星云”。"
                            + "\r\n        位于银河系中心的“人马座A”是一个高深莫测的无线电波源，同时也是一个强大的红外线和X射线辐射源。天文学家们早就猜测这些射线的产生可能与黑洞活动有关。德国科学家在2008年最终证实，与地球相距2.6万光年的“人马座A”确实是一个质量超大的黑洞（质量约为太阳的400万倍）。"
                            + "\r\n        天体名称：人马座，又称射手座"
                            + "\r\n        拉丁名：Sagittarius"
                            + "\r\n        亮星数目：5"
                            + "\r\n        最亮星：箕宿三（人马座ε），视星等1.85"
                            + "\r\n        最佳观测时间：8月"
                            + "\r\n        最佳观测纬度：44°N-90°S";
            introduction[14] = "        天蝎座，黄道十二宫之一，太阳在每年11.25-11.29运行到天蝎座。它位于南半球，在西面的天秤座与东面的人马座之间，是一个接近银河中心的大星座。纬度变化位于+40°和−90°之间可全见。夏季出现在南方天空，蝎尾指向东南，在蛇头、人马、天秤等星座之间。α星（心宿二）是红色的1等星。疏散星团M6和M7肉眼均可见，座内有亮于4等的星22颗。"
                            + "\r\n        天蝎座是夏天最显眼的星座，它里面亮星云集，光是亮于4m的星就有20多颗。天蝎座又大亮星又多，简直可以说是夏夜星座的代表。夏天晚上八九点钟的时候，南方离地平线不很高的地方有一颗亮星，这就是天蝎座α星（心宿二）。不过，天蝎座只在黄道上占据了短短7°的范围，是十二个星座中黄道经过最短的一个。"
                            + "\r\n        天蝎座从α星开始一直到长长的蝎尾都沉浸在茫茫银河里。α星恰恰位于蝎子的胸部，因而西方称它是“天蝎之心”。有趣的是，在我国古代，正好把天蝎座α星划在二十八宿的心宿里，叫作“心宿二”。你看，东西方的天文学家们不谋而合了。"
                            + "\r\n        天体名称：天蝎座"
                            + "\r\n        拉丁名：Scorpius"
                            + "\r\n        亮星数目：9"
                            + "\r\n        最亮星：心宿二（天蝎座α），视星等0.96"
                            + "\r\n        最佳观测时间：7月"
                            + "\r\n        最佳观测纬度：44°N-90°S";
            introduction[15] = "        天秤座（Libra），9月23日——10月23日，黄道十三星座第七，位于室女座的东南方向，也属于黄道星座。星座中最亮的四颗3m星α、β、γ、σ组成了一个四边形，其中的β星又和春季大三角构成了一个大的菱形，就可以找到这个星座了。纬度变化位于+65°和−90°之间可全见。"
                            + "\r\n        天秤座原名为天平座，其图是天平（一种衡器）取其公平公正之意，故为天平座。"
                            + "\r\n        天秤座的群星自古就被认识到了，但托勒密并没有将其作为独立的星座。在托勒密星座中，天秤座的星属于天蝎座的蝎爪，天秤座α是天蝎座北方的爪“北螯”，天秤座β是天蝎座南方的爪“南螯”。人们将这群星看作一杆秤，象征着公平。但罗马人直到公元1世纪的恺撒时代，才发现太阳运行到此星座时，正是昼夜平分的秋分前后，因而将其独立划出。现在，由于地球岁差，秋分点已经移向了西方的室女座。在现在历元里，每年11月1日至11月23日太阳在天秤座运行。"
                            + "\r\n        天体名称：天秤座"
                            + "\r\n        拉丁名：Libra"
                            + "\r\n        亮星数目：2"
                            + "\r\n        最亮星：氐宿四（天秤座β），视星等2.6"
                            + "\r\n        最佳观测时间：6月"
                            + "\r\n        最佳观测纬度：60°N-90°S";
            introduction[16] = "        室女座（拉丁语：Virgo，天文符号：♍），是最大的黄道带星座，面积1294.43平方度，占全天面积的3.318%，在全天88个星座中，面积排行第二位，仅次于长蛇座。每年的春季太阳落山不久，它就出现在东方的地平线上，在春夏两季的夜空中室女座一直吐放着它的光芒。室女座中亮于5.5等的恒星有58颗，最亮星为角宿一（室女座α），视星等为0.98。每年4月11日子夜室女座中心经过上中天。现在的秋分点位于室女座β附近。"
                            + "\r\n        在古代星图上室女被画成一位长着双翅正在收割的女神，一手拿着一束麦穗，一手拿着镰刀。有时被想象为丰收女神；有时被认为是主持正义的女神，这时她旁边的天秤被认为是她称量人的善恶的天秤。顺着大熊座北斗勺把儿的弧线，就可以找到牧夫座α星，也就是大角。沿着这条曲线继续向南找，再经过差不多同样的长度，可以看见一颗亮星，这就是室女座α星，我国古代称为角宿一。连接北斗的α星和γ星，延长到七八倍远的地方也可以看到角宿一。"
                            + "\r\n        室女座星系团包括M49（椭圆）、M58（螺旋）、M59（椭圆）、M60（椭圆）、M61（螺旋）、M84（椭圆）、M86（椭圆）、M87（椭圆；著名的射电源）及M90（螺旋）。"
                            + "\r\n        天体名称：处女座（又名室女座）"
                            + "\r\n        拉丁名：Virgo"
                            + "\r\n        亮星数目：3"
                            + "\r\n        最亮星：角宿一（室女座α），视星等1.0"
                            + "\r\n        最佳观测时间：4月"
                            + "\r\n        最佳观测纬度：67°N-75°S";
            introduction[17] = "        狮子座（拉丁语：Leo）黄道带星座之一，面积946.96平方度，占全天面积的2.296%，在全天88个星座中，面积排行第十二位。狮子座中亮于5.5等的恒星有52颗，最亮星为轩辕十四（狮子座α），视星等为1.35。每年3月1日子夜狮子座中心经过上中天。"
                            + "\r\n        狮子座是一个明亮的星座，在春季星空中很容易辨认。其中，由南向北，轩辕十四（α）、轩辕十三（η）、轩辕十二（γ）、轩辕十一（ζ）、轩辕十（μ）及轩辕九（ε）组成了“镰刀”（或反写的问号）结构，它们代表了狮子的头、颈及鬃毛部份。"
                            + "\r\n        狮子座最亮星轩辕十四是一颗蓝白色恒星，视星等1.35，光度在全夜空中排第二十一位，它象征着狮子的心脏。因轩辕十四在黄道之上，偶尔会出现月掩轩辕十四的天文现象。 β星五帝座一是2.1等的白色恒星，也是一颗有变星。它与牧夫座的大角星及室女座的角宿一组成一个等边三角形，称为“春季大三角”。 γ星轩辕十二则是一对金橘色双星。"
                            + "\r\n        每年11月中旬，尤其是14、15两日的夜晚，在狮子座反写问号的ζ星附近，会有大量的流星出现，这就是著名的狮子座流星雨"
                            + "\r\n        天体名称：狮子座"
                            + "\r\n        拉丁名：Leonis"
                            + "\r\n        亮星数目：3"
                            + "\r\n        最亮星：轩辕十四（狮子座α），视星等1.4"
                            + "\r\n        最佳观测时间：4"
                            + "\r\n        最佳观测纬度：82°N-57°S";
            introduction[18] = "        巨蟹座（外文名：Cancer），黄道十二宫星座之一，面积505.87平方度，占全天面积的1.226%。巨蟹座中亮于5.5等的恒星有23颗，最亮星为柳宿增十（巨蟹座β），视星等为3.52。每年1月30日子夜巨蟹座中心经过上中天。巨蟹座位于双子座和狮子座之间、北方是天猫座、南面则是小犬座和长蛇座，是一个很暗的星座，没有亮于3等的恒星。"
                            + "\r\n        在巨蟹座中央的δ星附近（或是狮子座轩辕十四和双子座β星这两颗亮星之间），眼力好的人可以看到一小团白色的雾气，我国古代称之为“积尸气”，书中描述它：“如云非云，如星非星，见气而已。”直到望远镜发明以后人们才观测到，它原来是一个星团，天文学上称为“鬼星团”。这个星团的成员有200多颗，距离我们520光年。"
                            + "\r\n        天体名称：巨蟹座"
                            + "\r\n        拉丁名：Cancer"
                            + "\r\n        亮星数目：0"
                            + "\r\n        最亮星：柳宿增十（巨蟹座β），视星等3.52"
                            + "\r\n        最佳观测时间：3月"
                            + "\r\n        最佳观测纬度：+90°和-60°之间";


            theplanets[0] = new ThePlanets { ImagePath1 = "/Assets/d0.jpg", ImagePath2 = "/Assets/d0.jpg", MusicPath = "", introduction = introduction[0], story = Story[0], name = "水星 Mercury" };//水星
            theplanets[1] = new ThePlanets { ImagePath1 = "/Assets/d1.jpg", ImagePath2 = "/Assets/d1.jpg", MusicPath = "", introduction = introduction[1], story = Story[1], name = "金星 Venus" };//金星
            theplanets[2] = new ThePlanets { ImagePath1 = "/Assets/d2.jpg", ImagePath2 = "/Assets/d2.jpg", MusicPath = "", introduction = introduction[2], story = Story[2], name = "火星 Mars" };//火星
            theplanets[3] = new ThePlanets { ImagePath1 = "/Assets/d3.jpg", ImagePath2 = "/Assets/d3.jpg", MusicPath = "", introduction = introduction[3], story = Story[3], name = "木星 Jupiter" };//木星
            theplanets[4] = new ThePlanets { ImagePath1 = "/Assets/d4.jpg", ImagePath2 = "/Assets/d4.jpg", MusicPath = "", introduction = introduction[4], story = Story[4], name = "土星 Saturn" };//土星
            theplanets[5] = new ThePlanets { ImagePath1 = "/Assets/d5.jpg", ImagePath2 = "/Assets/d5.jpg", MusicPath = "", introduction = introduction[5], story = Story[5], name = "天王星 Uranus" };//天王星
            theplanets[6] = new ThePlanets { ImagePath1 = "/Assets/d6.jpg", ImagePath2 = "/Assets/d6.jpg", MusicPath = "", introduction = introduction[6], story = Story[6], name = "海王星 Neptune" };//海王星
            theplanets[7] = new ThePlanets { ImagePath1 = "/Assets/d7.jpg", ImagePath2 = "/Assets/story7.jpg", MusicPath = "", introduction = introduction[7], story = Story[7], name = "双子座 Gemini" };//双子座
            theplanets[8] = new ThePlanets { ImagePath1 = "/Assets/d8.jpg", ImagePath2 = "/Assets/story8.jpg", MusicPath = "", introduction = introduction[8], story = Story[8], name = "金牛座 Taurus" };//金牛座
            theplanets[9] = new ThePlanets { ImagePath1 = "/Assets/d9.jpg", ImagePath2 = "/Assets/story9.jpg", MusicPath = "", introduction = introduction[9], story = Story[9], name = "白羊座 Aries" };//白羊座
            theplanets[10] = new ThePlanets { ImagePath1 = "/Assets/d10.jpg", ImagePath2 = "/Assets/story10.jpg", MusicPath = "", introduction = introduction[10], story = Story[10], name = "双鱼座 Pisces" };//双鱼座
            theplanets[11] = new ThePlanets { ImagePath1 = "/Assets/d11.jpg", ImagePath2 = "/Assets/story11.jpg", MusicPath = "", introduction = introduction[11], story = Story[11], name = "水瓶座 Aquarius" };//水瓶座
            theplanets[12] = new ThePlanets { ImagePath1 = "/Assets/d12.jpg", ImagePath2 = "/Assets/story12.jpg", MusicPath = "", introduction = introduction[12], story = Story[12], name = "摩羯座 Capricornus" };//摩羯座
            theplanets[13] = new ThePlanets { ImagePath1 = "/Assets/d13.jpg", ImagePath2 = "/Assets/story13.jpg", MusicPath = "", introduction = introduction[13], story = Story[13], name = "射手座 Sagittarius" };//射手座
            theplanets[14] = new ThePlanets { ImagePath1 = "/Assets/d14.jpg", ImagePath2 = "/Assets/story14.jpg", MusicPath = "", introduction = introduction[14], story = Story[14], name = "天蝎座 Scorpio" };//天蝎座
            theplanets[15] = new ThePlanets { ImagePath1 = "/Assets/d15.jpg", ImagePath2 = "/Assets/story15.jpg", MusicPath = "", introduction = introduction[15], story = Story[15], name = "天秤座 Libra" };//天秤座
            theplanets[16] = new ThePlanets { ImagePath1 = "/Assets/d16.jpg", ImagePath2 = "/Assets/story16.jpg", MusicPath = "", introduction = introduction[16], story = Story[16], name = "处女座 Virgo" };//处女座
            theplanets[17] = new ThePlanets { ImagePath1 = "/Assets/d17.jpg", ImagePath2 = "/Assets/story17.jpg", MusicPath = "", introduction = introduction[17], story = Story[17], name = "狮子座 Leo" };//狮子座
            theplanets[18] = new ThePlanets { ImagePath1 = "/Assets/d18.jpg", ImagePath2 = "/Assets/story18.jpg", MusicPath = "", introduction = introduction[18], story = Story[18], name = "巨蟹座 Cancer" };//巨蟹座

            
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }



        #region NavigationHelper 注册

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            number = (int)e.Parameter;
            DataContext = theplanets[number];
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void MusicPlayer_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            switch (MusicPlayer.CurrentState)
            {
                case MediaElementState.Playing:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Playing;
                    break;

                case MediaElementState.Paused:
                     systemControls.PlaybackStatus = MediaPlaybackStatus.Paused;
                    break;

                case MediaElementState.Stopped:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Stopped;
                    break;

                case MediaElementState.Closed:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Closed;
                    break;
                default:
                    break;
            }
        }

        private void SystemControls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    //MusicPlayer.Source = new Uri("ms-assx:///Catch me if you can.mp3");
                    PlayMedia();
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    PauseMedia();
                    break;
                default:
                    break;
            }
        }

        async void PlayMedia()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    MusicPlayer.Play();
                });
        }

        async void PauseMedia()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                MusicPlayer.Pause();
            });
        }

        private void HelpAppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsAppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CollectionPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CollectionPage), MainPage.numberCollection);
        }


    }

}
