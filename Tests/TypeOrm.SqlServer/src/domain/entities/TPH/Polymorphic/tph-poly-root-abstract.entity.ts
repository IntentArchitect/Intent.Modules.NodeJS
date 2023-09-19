import { TphPoly_RootAbstract_Aggr } from './tph-poly-root-abstract-aggr.entity';
import { TphPoly_RootAbstract_Comp } from './tph-poly-root-abstract-comp.entity';
import { TphPoly_TopLevel } from './tph-poly-top-level.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne, OneToOne, JoinColumn } from 'typeorm';

@Entity('tph_poly_root_abstract')
export abstract class TphPoly_RootAbstract {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  abstractField: string;

  @Column({ nullable: true })
  polyToplevelId?: string;

  @Column({ nullable: true })
  polyRootabstractAggrId?: string;

  @ManyToOne(() => TphPoly_RootAbstract_Aggr, { cascade: ['insert', 'update'] })
  poly_RootAbstract_Aggr?: TphPoly_RootAbstract_Aggr;

  @OneToOne(() => TphPoly_RootAbstract_Comp, { cascade: true, eager: true })
  @JoinColumn()
  poly_RootAbstract_Comp?: TphPoly_RootAbstract_Comp;

  @ManyToOne(() => TphPoly_TopLevel, (poly_TopLevel) => poly_TopLevel.poly_RootAbstracts, { cascade: ['insert', 'update'] })
  poly_TopLevel?: TphPoly_TopLevel;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}