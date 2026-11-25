VERBOSE_BIN ?= verbose

RAYLIB_LIBS ?= -lraylib -lm

.PHONY: all demos test test-real run-particles run-solar run-breakout clean

all: demos

demos:
	@mkdir -p build
	$(VERBOSE_BIN) demos/01_particle_arena.vb -I src $(RAYLIB_LIBS) --no-run -o build/01_particle_arena
	$(VERBOSE_BIN) demos/02_solar_system_3d.vb -I src $(RAYLIB_LIBS) --no-run -o build/02_solar_system_3d
	$(VERBOSE_BIN) demos/03_arcade_breakout.vb -I src $(RAYLIB_LIBS) --no-run -o build/03_arcade_breakout
	@echo "[OK] All 3 pure-VerBose Raylib demos compiled against system libraylib -> build/"

run-particles:
	$(VERBOSE_BIN) demos/01_particle_arena.vb -I src $(RAYLIB_LIBS)

run-solar:
	$(VERBOSE_BIN) demos/02_solar_system_3d.vb -I src $(RAYLIB_LIBS)

run-breakout:
	$(VERBOSE_BIN) demos/03_arcade_breakout.vb -I src $(RAYLIB_LIBS)

# Headless CI/unit verification (100% pure VerBose mock — zero C files, no GPU/display needed)
test:
	$(VERBOSE_BIN) test tests/test_raylib_wrapper.vb -I tests/mock -I src -lm
	$(VERBOSE_BIN) demos/01_particle_arena.vb -I tests/mock -I src -lm
	$(VERBOSE_BIN) demos/02_solar_system_3d.vb -I tests/mock -I src -lm
	$(VERBOSE_BIN) demos/03_arcade_breakout.vb -I tests/mock -I src -lm

# Verify directly against installed system libraylib (zero custom C files)
test-real:
	$(VERBOSE_BIN) test tests/test_raylib_wrapper.vb -I src $(RAYLIB_LIBS)

clean:
	rm -rf build demos/*.ll tests/*.ll
