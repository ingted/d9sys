param([system.web.caching.cache] $cache, [object[]]$funs, [object[]]$params, [System.Collections.Specialized.NameValueCollection] $kv, [System.Collections.Specialized.NameValueCollection] $kv2)
try{
    switch($funs[0]){
        "debug" {
            $Response.ContentType = "application/json"            
            "{`"result`":`"failed`", `"response`":`"$([System.Net.WebUtility]::HtmlEncode(($Response.Headers | Out-String)))`"}"
        }
		"GetWindowMetadata" {
			$Response.ContentType = "application/json"
            $kvhash = @{}
            $kv.AllKeys | %{
                $kvhash[$_] = $kv[$_]
            }
            $kv2.AllKeys | %{
                $kvhash[$_] = $kv2[$_]
            }

			switch($kvhash["RequestType"]){
                "SelectDataSet" {
        			"{`"result`":`"success`", 
        			  `"data`":{`"Type`":`"SelectDataSet`",
        				  `"Mode`":`"TableColumn`",
        				  `"Elements`":[
        						{`"Name`":`"Tables`",
        						 `"Type`":`"List`",
                                 `"Order`":[1],
        						 `"API`":`"GetContent`",                                 
        						 `"Post`":[
        							{`"RequestType`":`"Table`",
        							 `"RequestTarget`":`"AllTables`",
                                     `"ReplyToken`":null}]},
        						{`"Name`":`"Columns`",
        						 `"Type`":`"ListList`",
                                 `"Order`":[2],
                                 `"ParentElement`":`"Tables`",
                                 `"ItemType`":`"MultipleSelectionForSave`",
        						 `"API`":`"GetContent`",                                 
        						 `"Post`":[
        							{`"RequestType`":`"TableColumn`",
        							 `"RequestTarget`":`"WorldBank`",
                                     `"ReplyToken`":null},
        							{`"RequestType`":`"TableColumn`",
        							 `"RequestTarget`":`"TWExchange`",
                                     `"ReplyToken`":null}]},
        						{`"Name`":`"DataSetName`",
        					     `"Type`":`"TitleTextBox`",
                                 `"Order`":[0],
                                 `"ActionType`":`"OnKeyUpTimout`",        						  
                                 `"API`":`"GetContent`",
        						 `"Post`":{        								
        						 	`"ReplyToken`":null,
        						 	`"RequestType`":`"CheckForUnique`",
                                    `"RequestTarget`":{`"DataSetName`":`"請回傳使用者自行命名的一組客製化資料集名稱，預設請產生一組GUID給他`"} 
        						 }
        						},
                                {`"Name`":`"ConfirmFilter`",
        					     `"Type`":`"Button`",
                                 `"Repeat`":`"ForEachRow`",
                                 `"RepeatFor`":`"Tables`",
                                 `"Order`":[1,0],
        						 `"ActionType`":`"OnClickGenWindowAndWaitForSubWindowClose`",
        						 `"PostBackAndGenNewWindow`":{
        						 	`"API`":`"GetWindowMetadata`",
                                    `"ReplyToken`":null,
        						 	`"RequestType`":`"ColumnFilterForDataSet`",
        						 	`"RequestTarget`":{`"DataSetName`":`"請根據使用者選的資料集設定回傳，記得要有ReplyToken，ReplyToken會在登入的時候拿到`"} 
        						 }
        						},
                                {`"Name`":`"ConfirmSelection`",
        					     `"Type`":`"Button`",
                                 `"Order`":[3],
        						 `"ActionType`":`"OnClick`",
        						 `"Post`":{
        						 	`"API`":`"GetContent`",
        						 	`"ReplyToken`":null,
        						 	`"RequestType`":`"SaveDataSet`",
        						 	`"RequestTarget`":{`"DataSetName`":`"請根據使用者選的資料集設定回傳(應該會有多東西)，記得要有ReplyToken，ReplyToken會在登入的時候拿到`"} 
        						 }
        						}
        					]
        				}
        			}"
                }
                "ColumnFilterForDataSet" {
                    "{`"result`":`"success`", 
        			  `"data`":{`"Type`":`"ColumnFilterForDataSet`",
        				  `"Mode`":`"FilterFilterGroup`",
        				  `"Elements`":[
        						{`"Name`":`"Columns`",
        						 `"Type`":`"List`",
                                 `"Order`":1,
        						 `"API`":`"InheritFromParentWindow`",                                 
        						 `"Post`":[
        							null]},
        						{`"Name`":`"Groups`",
        						 `"Type`":`"List`",
                                 `"Order`":4,
        						 `"API`":`"InheritFromParentWindow`",                                 
        						 `"Post`":[null]},
        						{`"Name`":`"DataSetFilterName`",
        					     `"Type`":`"TitleTextBox`",
                                 `"Order`":0,
                                 `"ActionType`":`"OnKeyUpTimout`",        						  
                                 `"API`":`"GetContent`",
        						 `"Post`":{        								
        						 	`"ReplyToken`":null,
        						 	`"RequestType`":`"CheckForUnique`",
                                    `"RequestTarget`":{`"DataSetFilterName`":`"請回傳使用者自行命名的一組客製化資料集名稱，預設請產生一組GUID給他`"} 
        						 }
        						},
                                {`"Name`":`"ColumnFilter`",
        					     `"Type`":`"TextBox`",
                                 `"Repeat`":`"ForEachRow`",
                                 `"RepeatFor`":`"Columns`",
                                 `"Order`":[1,0],
        						 `"ActionType`":`"ConcatnateAllRows`"
        						},
                                {`"Name`":`"ColumnFilterAndOr`",
        					     `"Type`":`"RadioButton`",
                                 `"Repeat`":`"ForEachRow`",
                                 `"RepeatFor`":`"Columns`",
                                 `"Order`":[1,1],
        						 `"ActionType`":`"ConcatnateAllRows`"
        						},
                                {`"Name`":`"GroupsAndOr`",
        					     `"Type`":`"RadioButton`",
                                 `"Repeat`":`"ForEachRow`",
                                 `"RepeatFor`":`"Groups`",
                                 `"Order`":[4,0],
        						 `"ActionType`":`"ConcatnateAllRows`"
        						},
                                {`"Name`":`"AddAs`",
        					     `"Type`":`"Button`",
                                 `"Order`":[2],
        						 `"ActionType`":`"OnClickAddToListWithName`",
                                 `"ActionSource`":[{`"Name`":[3],`"Value`":[[1,0],[1,1]]}],
                                 `"ActionTarget`":[[4,0],[4,1]]
        						},
                                {`"Name`":`"ConfirmGroup`",
        					     `"Type`":`"Button`",
                                 `"Order`":[5],
        						 `"ActionType`":`"OnClickConfirmForParent`",
                                 `"ActionSource`":[{`"DataSet`":[`"WorldBank`"],`"Value`":[`"這邊請將串好的group filter字串放在某變數，並關閉此window`"]}],
                                 `"ActionTarget`":[-1]
        						},
                                {`"Name`":`"GroupName`",
        					     `"Type`":`"TextBox`",
                                 `"Order`":[3]
        						}
        					]
        				}
        			}"
                }
                DEFAULT {
                    "{`"result`":`"failed`"}"
                }
            }
		}

		"GetContent" {
			$Response.ContentType = "application/json"
			$kvhash = @{}
            $kv.AllKeys | %{
                $kvhash[$_] = $kv[$_]
            }
            $kv2.AllKeys | %{
                $kvhash[$_] = $kv2[$_]
            }

			switch($kvhash["RequestType"]){
				"Table" {
					switch($kvhash["RequestTarget"]){
						"AllTables" {
							"{`"result`":`"success`", 
							  `"data`":{
								`"Name`":`"AllTables`",
								`"Type`":`"List`",
								`"Columns`":[
					  			`"WorldBank`", `"TWExchange`"
							  ]}
							}"
						}
						default {
							"{`"result`":`"failed`",`"reason`":`"Target not existed`"}"
						}
					}
				}
				"TableColumn" {
					switch($kvhash["RequestTarget"]){
						"WorldBank" {
							"{`"result`":`"success`", 
							  `"data`":{
								  `"Name`":`"Columns Of Table WorldBank`",
								  `"Type`":`"List`",
								  `"Columns`":[
									`"Population`", `"GDP`", `"MarketValue`"
								]}
							}"
						}
						"TWExchange" {
							"{`"result`":`"success`", 
							  `"data`":{
								  `"Name`":`"Columns Of Table TWExchange`",
								  `"Type`":`"List`",
								  `"Columns`":[
									`"Bid`", `"Sale`", `"Close`", `"Volume`", `"AccumulatedVoulme`"
								]}
							}"
						}
						default {
							"{`"result`":`"failed`",`"reason`":`"table not existed`"}"
						}
					}
				}
				default {
					"{`"result`":`"failed`"}"
				}
			}
		}
        default {
            $Response.ContentType = "application/json"
            $kvhash = @{}
            $kv.AllKeys | %{
                $kvhash[$_] = $kv[$_]
            }
            $kv2.AllKeys | %{
                $kvhash[$_] = $kv2[$_]
            }
            #$funs[0]
            #$kv2
            try{
                $rval = . "C:\git\PoshToolChain\Scripts\d9System\count_table_rows_and_size.ps1" -funcName $funs[0] -d9params $kvhash
                $result = 1
                if($rval.count -le 1){
                    "{`"result`":`"$result`", `"data`":$rval}"
                } else {
                    "{`"result`":`"$result`", `"data`":{`"values`":$rval}}"
                }
            } catch {
                $result = 0
                #"{`"result`":`"$result`", `"reason`":`"$([System.Net.WebUtility]::HtmlEncode(($_ | Out-String)))`"}"
                "{`"result`":`"$result`", `"reason`":`"$(($_ | Out-String))`"}"
            }
        }
    }
} catch {
    $Response.ContentType = "application/json"
    "{`"result`":`"failed`", `"reason`":`"$($_.tostring())`"}"
    
}