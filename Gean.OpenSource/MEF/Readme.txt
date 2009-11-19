托管扩展性框架是什么？

“托管扩展性框架（Managed Extensibility Framework，简称MEF），是.NET的一个新的类库，旨在促成应用和组件更大的重用。通过使用MEF，.NET应用将能从是静态编译的而变成可动态组合的。如果你正在建造可扩展的应用，可扩展的框架和应用扩展，那么MEF就可为你所用。”

今年早些时候，微软成立了一个应用框架核心开发团队（Application Framework Core team），其宗旨是在应用框架空间（WinForms, ASP.NET, WPF, Silverlight）起和基础类库（BCL）开发团队在平台底层方面一样的作用。

基础类库开发团队在负责平台底层层次上减少重复和提供共同的抽象诸多方面起了很大的作用，但在高层次方面微软还没有类似团队负责处理同类问题。于是乎，造成了一些不幸的重复（譬如每个应用模型都有若干个数据绑定模型，WPF和WF有着不同的依赖属性系统），且在应用模型代码方面缺乏共同的抽象。

于是应用框架核心开发团队就应运而生了。

这个团队的第一个具体项目就是“托管扩展性框架（MEF）”，他们发现，在.NET框架本身以及日趋托管的应用（象Visual Studio）中越来越多的地方，需要提供或者已经提供了钩子（hook），可为第三方扩展所用。譬如使用TraceSource API的TraceListener插件，Visual Studio代码分析的可插拔规则等等。

但在不存在一个内置的扩展性框架的情形下，如果开发人员想要提供这样的扩展，只好通过提供定制的机制来实现，于是造成重复劳动。他们希望MEF可以中止这样的重复，在框架和应用中鼓励以及促成建立在MEF之上的更多的扩展性。

六月初，他们推出了第一个CTP版本，九月份的这个版本是第二个版本。该版本包括完整的框架代码，还有三个例程(类似Outlook客户端的MEFlook，类似Tetris，形状可通过插件形式扩展的游戏MEFTris，以及可扩展的文件管理器）。
MEF与IoC容器的区别

从表面上，MEF有点类似IoC容器，但MEF并不是IoC容器。在这一点上，很多人都很困惑。

Oren Eini在他的博客中指出，MEF实际上是个组合框架（composition framework），而且定位客户是“大的应用”，两者合一，即可理解MEF的本质。

组合框架和IoC容器在表面上看很相似，都是以自动化的方式管理应用的依赖性，但其区别则在于细节上。IoC容器很久以前就不仅仅是管理依赖性了，它们还负责对象的生命周期，对象代理，面向aspect，事件聚合，事务管理等等东西。但组合框架则着重于单一个目的：依赖管理。听上去组合框架好像所做极有限，但其实不是这样。光从依赖管理来说，IoC容器往往是静态的，不透明的，而MEF则使得依赖管理变成一个动态的，透明的过程。

MEF的第二个方面，其定位是“大的应用”，极其大的应用，其中第一个客户大概就是Visual Studio本身。MEF提供的这些功能，
-不用装载程序集即可查询元数据
-可以静态地核实所有组件的依赖图和拒绝那些会造成系统处于不合法状态的组件
-契约适配器
-提供一套发现机制，用于定位和装载扩展
-允许附件元数据的标记设置，用于辅助查询和过滤

都是围绕着依赖管理这个主题的，但也大概来自Visual Studio是第一个客户的需求。因为Visual Studio涉及的组件成千上万，需要这样的东西，MEF是设计来处理这样的场景的。

Sidar Ok用了一个具体的例子来说明MEF并不是IoC容器，参考
What is this Managed Extensibility Framework thing all about ?
http://www.sidarok.com/web/blog/content/2008/09/26/what-is-this-managed-extensibility-framework-thing-all-about.html

另一件需要指出的事情是，MEF将是.Net 4.0的一部分，所以MEF将为.NET框架本身所用。

【参考】
1. Oren Eini的《The Managed Extensibility Framework》
http://ayende.com/Blog/archive/2008/09/25/the-managed-extensibility-framework.aspx

2. Glenn Block的《What is the Managed Extensibility Framework?》
http://codebetter.com/blogs/glenn.block/archive/2008/09/25/what-is-the-managed-extensibility-framework.aspx

3. Krzysztof Cwalina的《Managed Extensibility Framework》
http://blogs.msdn.com/kcwalina/archive/2008/04/25/MEF.aspx