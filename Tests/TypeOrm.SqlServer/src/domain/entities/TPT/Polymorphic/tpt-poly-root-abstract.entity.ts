import { TptPoly_RootAbstract_Aggr } from './tpt-poly-root-abstract-aggr.entity';
import { TptPoly_RootAbstract_Comp } from './tpt-poly-root-abstract-comp.entity';
import { TptPoly_TopLevel } from './tpt-poly-top-level.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne, OneToOne, JoinColumn } from 'typeorm';

@Entity('tpt_poly_root_abstract')
export abstract class TptPoly_RootAbstract {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  abstractField: string;

  @Column({ nullable: true })
  polyToplevelId?: string;

  @Column({ nullable: true })
  polyRootabstractAggrId?: string;

  @ManyToOne(() => TptPoly_RootAbstract_Aggr, { cascade: ['insert', 'update'] })
  poly_RootAbstract_Aggr?: TptPoly_RootAbstract_Aggr;

  @OneToOne(() => TptPoly_RootAbstract_Comp, { cascade: true, eager: true })
  @JoinColumn()
  poly_RootAbstract_Comp?: TptPoly_RootAbstract_Comp;

  @ManyToOne(() => TptPoly_TopLevel, (poly_TopLevel) => poly_TopLevel.poly_RootAbstracts, { cascade: ['insert', 'update'] })
  poly_TopLevel?: TptPoly_TopLevel;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}