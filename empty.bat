for /f "usebackq" %%d in (`"dir /ad/b/s/x | sort /R"`) do copy gitkeep.git %%d

